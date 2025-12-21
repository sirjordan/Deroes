using Godot;

public partial class Fog : TileMapLayer
{
	[Export] public int FogTileId { get; set; } = 1;
	[Export] public int SemiFogTileId { get; set; } = 2;
	[Export] public int SightRange { get; set; } = 8;

	private TileMapLayer _terrain;
	private Vector2I _playerPosition;

	public override void _Ready()
	{
		SignalManager.Instance.Settings_FogToggle += Settings_FogToggle;
		SignalManager.Instance.PlayerMoving += PlayerMoving;

		_playerPosition = Vector2I.Zero;
		_terrain = GetNode<TileMapLayer>("../Ground");

		// Cover all terrain tiles with Fog
		foreach (var vcell in _terrain.GetUsedCells())
		{
			SetCell(vcell, FogTileId, new Vector2I(0, 0));
		}
	}

	private void PlayerMoving(Vector2 playerPosition)
	{
		// Reveal only if player is changing position tile
		Vector2I localPos = LocalToMap(ToLocal(playerPosition));
		if (_playerPosition != localPos)
		{
			_playerPosition = localPos;

			Reveal(playerPosition, SightRange);
		}
	}

	private void Reveal(Vector2 pos, int radius)
	{
		Vector2 localPos = ToLocal(pos);
		Vector2I center = LocalToMap(localPos);

		for (int y = -radius; y <= radius; y++)
		{
			for (int x = -radius; x <= radius; x++)
			{
				int manhattan = Mathf.Abs(x) + Mathf.Abs(y);
				if (manhattan > radius)
					continue;

				Vector2I cell = center + new Vector2I(x, y);

				EraseCell(cell);

				// Edge 
				//var neighbors = GetSurroundingCells(cell);
				//var allAreEmpty = neighbors.ToList().TrueForAll(n => GetCellSourceId(n) == -1);		
				//if (!allAreEmpty)
				//{
				//	SetCell(cell, SemiFogTileId, Vector2I.Zero);
				//}

				//if (manhattan == radius)
				//{
				//	//SetCell(cell, SemiFogTileId, Vector2I.Zero);
				//}
			}
		}
	}

	private void Settings_FogToggle(bool toggle)
	{
		Visible = toggle;
	}
}
