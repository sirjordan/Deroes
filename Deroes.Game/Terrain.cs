using Godot;
using System;

public partial class Terrain : TileMapLayer
{
	[Export] public int Transparent_TileId { get; set; } = 3;

	public TileMapLayer Scenery { get { return GetNode<TileMapLayer>("../Scenery"); } }

	public override void _Ready()
	{
		var filled_tiles = GetUsedCells();
		foreach (var filled_tile in filled_tiles)
		{
			var neighboring_tiles = GetSurroundingCells(filled_tile);
			foreach (var neighbor in neighboring_tiles)
			{
				if (GetCellSourceId(neighbor) == -1)
				{
					SetCell(neighbor, Transparent_TileId, Vector2I.Zero);
				}
			}
		}
	}

	public override bool _UseTileDataRuntimeUpdate(Vector2I coords)
	{
		if (Scenery != null && Scenery.GetUsedCells().Contains(coords))
		{
			GD.Print("Contains");
			return true;
		}

		return false;
	}

	public override void _TileDataRuntimeUpdate(Vector2I coords, TileData tileData)
	{
		if (Scenery != null && Scenery.GetUsedCells().Contains(coords))
		{
			GD.Print("Set");
			tileData.SetNavigationPolygon(0, null);
		}
	}
}
