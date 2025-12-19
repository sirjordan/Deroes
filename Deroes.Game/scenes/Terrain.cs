using Godot;

public partial class Terrain : TileMapLayer
{
	[Export] public int Transparent_TileId { get; set; } = 3;

	public override void _Ready()
	{
		// Fill edges with transparent tile (world border)
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
}
