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
		_picked = null;
		_originalScale = Scale;
		PivotOffset = Size / 2;

		// Clone the panel style
		_style = (StyleBoxFlat)GetThemeStylebox("panel").Duplicate();
		AddThemeStyleboxOverride("panel", _style);

		DisableShadow();

		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;
		GuiInput += OnGuiInput;
		Resized += () => PivotOffset = Size / 2;
	}

	public override void _Process(double delta)
	{
		if (_picked != null)
		{
			// Move the picked item with the mouse
			_picked.GlobalPosition = GetViewport().GetMousePosition() - _picked.Size * 0.5f;
		}
	}

	private void OnGuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			var cellItem = GetChildren().FirstOrDefault(c => c is Item) as Item;
			if (_picked == null)
			{
				if (cellItem != null)
				{
					// Pick
					Pick(cellItem);

				}
			}
			else
			{
				if (cellItem == null)
				{
					// Place
					Place();
				}
				else
				{
					if (_picked == cellItem)
					{
						// Return it if the same cell
						Unpick();
					}
					else
					{
						// Replace and pick
						Place();
						Pick(cellItem);
					}
				}
			}
		}

		void Unpick()
		{
			_picked.Position = Vector2.Zero;
			_picked.Scale = _originalScale;
			_picked.ZIndex -= 1;
			_picked = null;
		}

		void Pick(Item cellItem)
		{
			cellItem.Scale = HoverScale;
			_picked = cellItem;
			_picked.ZIndex += 1;
		}

		void Place()
		{
			_picked.GetParent().RemoveChild(_picked);
			AddChild(_picked);
			Unpick();
		}
	}

	private void OnMouseEntered()
	{
		EnableShadow();
		StartTween(HoverScale);
		ZIndex += 1;
	}

	private void OnMouseExited()
	{
		DisableShadow();
		StartTween(_originalScale);
		ZIndex -= 1;
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
