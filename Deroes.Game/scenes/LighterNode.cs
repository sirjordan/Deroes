using Godot;
using System;

public partial class LighterNode : Node2D
{
	[Export] public float MinEnergy { get; set; } = 0.25f;
	[Export] public float MaxEnergy { get; set; } = 0.6f;
	[Export] public float Speed { get; set; } = 0.3f;


	private bool _glowing = false;
	private PointLight2D _light; 

	public override void _Ready()
	{
		var timer = GetNode<Timer>("Timer");
		timer.Timeout += Timer_Timeout;

		_light = GetNode<PointLight2D>("PointLight2D");
	}

	public override void _Process(double delta)
	{
		if (_glowing)
		{
			if (_light.Energy > MinEnergy)
			{
				_light.Energy -= (float)delta * Speed;
			}
		}
		else
		{
			if (_light.Energy < MaxEnergy)
			{
				_light.Energy += (float)delta * Speed;
			}
		}
	}

	private void Timer_Timeout()
	{
		var chance = 0.5;
		var rnd = new Random();
		var prob = rnd.NextDouble();

		if (prob < chance)
		{
			_glowing = !_glowing;
		}
	}
}
