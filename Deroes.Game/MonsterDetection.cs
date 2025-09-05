using Godot;

public partial class MonsterDetection : CollisionShape2D
{
	public override void _Ready()
	{
		var area = GetNode<Area2D>("../../DetectionZone");
		area.BodyEntered += Area_BodyEntered;
		area.BodyExited += Area_BodyExited;
	}

	private void Area_BodyEntered(Node2D body)
	{
		GD.Print("Entered");
	}

	private void Area_BodyExited(Node2D body)
	{
		GD.Print("Exited");
	}
}
