using Godot;
using GodotPlugins.Game;
using System;

public partial class Rain : GpuParticles2D
{
	private DirectionalLight2D _lightining;
	private byte _strikes = 0;
	private readonly byte _max_strikes = 2;
	private bool _strike = false;

	public override void _Ready()
	{
		var timer = GetNode<Timer>("LightiningTime");
		timer.Timeout += LightiningStrike;

		_lightining = GetNode<DirectionalLight2D>("DirectionalLight2D");
		_lightining.Enabled = false;

		// Center the position and cover full screen
		var viewportSize = GetViewportRect().Size;
		 Position = viewportSize / 2;
		if (ProcessMaterial is ParticleProcessMaterial material)
		{
			material.EmissionShape = ParticleProcessMaterial.EmissionShapeEnum.Box;
			material.EmissionBoxExtents = new Vector3(
			   viewportSize.X / 2f,
			   viewportSize.Y / 2f,
			   0f // not used in 2D
		   );
		}
	}

	public override void _Process(double delta)
	{
		if (_strike)
		{
			if (_strikes <= _max_strikes)
			{
				_lightining.Enabled = !_lightining.Enabled;
				_strikes++;
			}
			else
			{
				_lightining.Enabled = false;
				_strikes = 0;
				_strike = false;
			}
		}
	}

	private void LightiningStrike()
	{
		if (Emitting)
		{
			// Strike a lighining with changce of {chance} %
			var chance = 0.25;
			var rnd = new Random();
			var prob = rnd.NextDouble();

			if (prob < chance)
			{
				_strike = true;
			}
		}
	}
}
