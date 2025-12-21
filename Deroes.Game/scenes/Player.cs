using Godot;

public partial class Player : CharacterBody2D
{
	private NavigationAgent2D _agent;
	private Vector2 _dirOrientation;
	private float _movementSpeed;

	[Export] public float WalkSpeed { get; set; } = 180f;
	[Export] public float RunSpeed { get; set; } = 260f;
	[Export] public AnimatedSprite2D MovementSprite { get; set; }

	public override void _Ready()
	{
		_agent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		_movementSpeed = WalkSpeed;
	}

	public override void _Input(InputEvent @event)
	{
		// If the click is over a Control, do NOT move the character
		if (GetViewport().GuiGetHoveredControl() != null) 
			return;

		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			var target = GetGlobalMousePosition();

			_agent.TargetPosition = target;
			_dirOrientation = (target - GlobalPosition).Normalized();
			_movementSpeed = mouseEvent.DoubleClick ? RunSpeed : WalkSpeed;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		// Handle moving and directional sprites
		if (_agent.IsNavigationFinished())
		{
			// Still

			MovementSprite.Play("default");

			Velocity = Vector2.Zero;
			MoveAndSlide();
		}
		else
		{
			// Moving

			var dir_sprite = GetDirectionByAngle(_dirOrientation);
			MovementSprite.Play(dir_sprite);

			// Cartesian target from Navigation
			Vector2 nextPosCartesian = _agent.GetNextPathPosition();

			// Convert both current position and target to isometric space
			Vector2 currentIso = ToIso(GlobalPosition);
			Vector2 targetIso = ToIso(nextPosCartesian);

			// Compute direction in iso space
			Vector2 directionIso = (targetIso - currentIso).Normalized();

			// Option A: Move directly in iso space (and convert back to world velocity)
			Vector2 velocityCartesian = FromIso(directionIso) * _movementSpeed;

			Velocity = velocityCartesian;
			MoveAndSlide();

			_dirOrientation = (nextPosCartesian - GlobalPosition).Normalized();
		}

		UpdateVisibility();
	}

	/// <summary>
	/// Convert world grid position (x, y) → isometric screen position
	/// </summary>
	private static Vector2 ToIso(Vector2 cartesian)
	{
		return new Vector2(
			cartesian.X - cartesian.Y,
			(cartesian.X + cartesian.Y) / 2
		);
	}

	/// <summary>
	/// Convert isometric screen position → world grid position
	/// </summary>
	private static Vector2 FromIso(Vector2 iso)
	{
		return new Vector2(
			(2 * iso.Y + iso.X) / 2,
			(2 * iso.Y - iso.X) / 2
		);
	}

	private static string GetDirectionByAngle(Vector2 directionVector)
	{
		float angle = Mathf.Atan2(-directionVector.Y, directionVector.X);
		var deg = Mathf.RadToDeg(angle);

		if (deg < 0)
			deg += 360;

		if (deg >= 337.5f || deg < 22.5f)
			return "E";
		else if (deg < 67.5f)
			return "NE";
		else if (deg < 112.5f)
			return "N";
		else if (deg < 157.5f)
			return "NW";
		else if (deg < 202.5f)
			return "W";
		else if (deg < 247.5f)
			return "SW";
		else if (deg < 292.5f)
			return "S";
		else
			return "SE";
	}

	public void UpdateVisibility()
	{
		SignalManager.Instance.EmitSignal(SignalManager.SignalName.PlayerMoving, GlobalPosition);
	}
}
