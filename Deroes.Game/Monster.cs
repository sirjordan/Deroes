using Godot;
using System;

public partial class Monster : CharacterBody2D
{
	public override void _Ready()
	{
		var idle = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		idle.Play();
	}
}
