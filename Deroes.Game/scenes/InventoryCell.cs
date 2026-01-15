using Godot;

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
