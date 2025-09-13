using Godot;
using System;

public partial class DetectionZone : Area2D
{
	private CharacterBody2D _monster;

	public override void _Ready()
	{
		BodyEntered += Area_BodyEntered;
		BodyExited += Area_BodyExited;
		_monster = GetNode<CharacterBody2D>("../Monster");
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
