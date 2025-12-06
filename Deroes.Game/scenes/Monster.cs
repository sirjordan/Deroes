using Godot;
using System;

public partial class Monster : CharacterBody2D
{
	private NavigationAgent2D _agent;

	[Export] public float Speed { get; set; } = 180f;

	public override void _Ready()
	{
		var idle = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		idle.Play();

		_agent = GetNode<NavigationAgent2D>("NavigationAgent2D");
	}
}
