using Godot;
using System;

public partial class Scenery : TileMapLayer
{
	public override void _Ready()
	{
		//foreach (var c in GetUsedCells())
		//{
		//	var obs = new NavigationObstacle2D();
		//	obs.Position = new Vector2I(c.X, c.Y);
		//	obs.Radius = 50f;
		//	obs.AvoidanceLayers = 1;

		//	AddChild(obs);

		//	GD.Print("NavObsticle");
		//}
	}
}
