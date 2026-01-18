using Godot;
using System;

public partial class LighterNode : Node2D
{
	[Export] public float MinEnergy { get; set; } = 0.5f;
	[Export] public float MaxEnergy { get; set; } = 0.6f;
	[Export] public float Speed { get; set; } = 0.3f;

	private bool _glowing;
	private PointLight2D _light;
	private Random _rnd;

	public override void _Ready()
	{
		var timer = GetNode<Timer>("Timer");
		timer.Timeout += Timer_Timeout;

		_light = GetNode<PointLight2D>("PointLight2D");

		_rnd = new Random();
		_glowing = false;
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
		var chance = 0.5f;
		var prob = (float)_rnd.NextDouble();

		if (prob < chance)
		{
			_glowing = !_glowing;
		}

		Position = new Vector2(prob / 2, prob / 2);
	}
}
