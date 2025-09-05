using Godot;

public partial class Area2d : Area2D
{
	public override void _Ready()
	{
		BodyEntered += Area2d_BodyEntered;
		BodyExited += Area2d_BodyExited;
		Visible = true;
	}

	private void Area2d_BodyEntered(Node2D body)
	{
		if (body is CharacterBody2D)
		{
			//Visible = false;
			Modulate = new Color(1, 1, 1, 0.4f);
		}
	}

	private void Area2d_BodyExited(Node2D body)
	{
		if (body is CharacterBody2D)
		{
			Modulate = new Color(1, 1, 1, 1);
			//Visible = true;
		}
	}
}
