using Godot;

public partial class SelectionCircle : Line2D
{
	[Export] public float RadiusX = 36; // width
	[Export] public float RadiusY = 18f;  // height 
	[Export] public int Segments = 48;

	public override void _Ready()
	{
		RedrawOval();
	}

	public override void _Process(double delta)
	{
		float pulse = 1.0f + Mathf.Sin(Time.GetTicksMsec() * 0.005f) * 0.05f;
		Scale = new Vector2(pulse, pulse);
	}

	public void RedrawOval()
	{
		ClearPoints();

		for (int i = 0; i < Segments; i++)
		{
			float t = Mathf.Tau * i / Segments;
			float x = Mathf.Cos(t) * RadiusX;
			float y = Mathf.Sin(t) * RadiusY;
			AddPoint(new Vector2(x, y));
		}
	}
}
