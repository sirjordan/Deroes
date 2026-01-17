using Godot;
using System.Linq;

public partial class InventoryCell : Panel
{
	[Export] public Vector2 HoverScale = new Vector2(1.25f, 1.25f);
	[Export] public float TweenTime = 0.15f;

	[Export] public int ShadowSize = 6;
	[Export] public Vector2 ShadowOffset = new(0, 0);
	[Export] public Color ShadowColor = new(0, 0, 0, 0.35f);

	private Tween _tween;
	private Vector2 _originalScale;
	private StyleBoxFlat _style;

	private static Item _picked;

	public override void _Ready()
	{
		_originalScale = Scale;
		PivotOffset = Size / 2;
		Resized += () => PivotOffset = Size / 2;

		// Clone the panel style
		_style = (StyleBoxFlat)GetThemeStylebox("panel").Duplicate();
		AddThemeStyleboxOverride("panel", _style);

		DisableShadow();

		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;
		GuiInput += InventoryCell_GuiInput;

		_picked = null;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_picked != null)
		{
			// Move the picked item with the mouse
			_picked.GlobalPosition = GetViewport().GetMousePosition() - _picked.Size * 0.5f;
		}
	}

	private void InventoryCell_GuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			var cellItem = GetChildren().FirstOrDefault(c => c is Item) as Item;
			if (_picked == null)
			{
				if (cellItem != null)
				{
					// Pick
					cellItem.Scale = HoverScale;
					_picked = cellItem;
				}
			}
			else
			{
				if (cellItem == null)
				{
					// Place
					_picked.GetParent().RemoveChild(_picked);
					AddChild(_picked);
					_picked.Position = Vector2.Zero;
					_picked.Scale = _originalScale;
					_picked = null;
				}
				else
				{
					// Replace and pick
				}
			}
		}
	}

	private void OnMouseEntered()
	{
		EnableShadow();
		StartTween(HoverScale);
	}

	private void OnMouseExited()
	{
		DisableShadow();
		StartTween(_originalScale);
	}

	private void StartTween(Vector2 targetScale)
	{
		_tween?.Kill();
		_tween = CreateTween();
		_tween.SetEase(Tween.EaseType.Out);
		_tween.SetTrans(Tween.TransitionType.Quad);
		_tween.TweenProperty(this, "scale", targetScale, TweenTime);
	}

	private void EnableShadow()
	{
		_style.ShadowSize = ShadowSize;
		_style.ShadowOffset = ShadowOffset;
		_style.ShadowColor = ShadowColor;
	}

	private void DisableShadow()
	{
		_style.ShadowSize = 0;
	}
}
