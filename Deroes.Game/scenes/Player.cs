using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private NavigationAgent2D _navAgent;
	private Vector2 _dirOrientation;
	private float _movementSpeed;
	private Vector2 _playerPosition;

	// Stairs only
	private TileMapLayer _ground;
	public float _stepHeight = 4f;     
	public float _stepSpeed = 10f;   
	private float _stepTimer = 0f;

	[Export] public float WalkSpeed { get; set; } = 180f;
	[Export] public float RunSpeed { get; set; } = 260f;
	[Export] public AnimatedSprite2D MovementSprite { get; set; }
	[Export] public int SightRange { get; set; } = 8;

	public override void _Ready()
	{
		_navAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		_ground = GetNode<TileMapLayer>("../Ground");
		_movementSpeed = WalkSpeed;

		RevealMap();
	}

	public override void _Input(InputEvent @event)
	{
		// If the click is over a Control, do NOT move the character
		if (GetViewport().GuiGetHoveredControl() != null)
		{
			return;
		}

		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			var target = GetGlobalMousePosition();

			_navAgent.TargetPosition = target;
			_dirOrientation = (target - GlobalPosition).Normalized();
			_movementSpeed = mouseEvent.DoubleClick ? RunSpeed : WalkSpeed;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_navAgent.IsNavigationFinished())
		{
			Stand();
		}
		else
		{
			CheckStairs(delta);
			Move();
			RevealMap();
		}
	}
	private void Stand()
	{
		MovementSprite.Play("default");

		Velocity = Vector2.Zero;
		MoveAndSlide();
	}

	private void Move()
	{
		var dir_sprite = GetDirectionByAngle(_dirOrientation);
		MovementSprite.Play(dir_sprite);

		// Cartesian target from Navigation
		Vector2 nextPosCartesian = _navAgent.GetNextPathPosition();

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

	private void RevealMap()
	{
		// Reveal only if player is changing position
		if (_playerPosition != GlobalPosition)
		{
			_playerPosition = GlobalPosition;
			SignalManager.Instance.EmitSignal(SignalManager.SignalName.RevealMap, GlobalPosition, SightRange);
		}
	}

	private void CheckStairs(double delta)
	{
		Vector2I tilePos = _ground.LocalToMap(GlobalPosition);
		TileData data = _ground.GetCellTileData(tilePos);

		bool onStairs = data != null && (string)data.GetCustomData("type") == "stairs";

		if (onStairs && Velocity.Length() > 0.1f)
		{
			_movementSpeed = WalkSpeed / 1.75f;
			_stepTimer += (float)delta * _stepSpeed;

			float step = Mathf.Floor(_stepTimer) % 2 == 0 ? 0f : _stepHeight;
			MovementSprite.Position = new Vector2(MovementSprite.Position.X, step);
		}
		else
		{
			_movementSpeed = WalkSpeed;
			_stepTimer = 0f;

			MovementSprite.Position = Vector2.Zero;
		}
	}
}
