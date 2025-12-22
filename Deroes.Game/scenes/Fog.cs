using Godot;

public partial class Fog : TileMapLayer
{
	[Export] public int FogTileId { get; set; } = 1;
	[Export] public int SemiFogTileId { get; set; } = 2;

	private TileMapLayer _terrain;
	private Vector2I _lastRevealed;

	public override void _Ready()
	{
		SignalManager.Instance.Settings_FogToggle += Settings_FogToggle;
		SignalManager.Instance.RevealMap += RevealMap;

		_lastRevealed = Vector2I.Zero;
		_terrain = GetNode<TileMapLayer>("../Ground");

		// Cover all terrain tiles with Fog
		foreach (var vcell in _terrain.GetUsedCells())
		{
			SetCell(vcell, FogTileId, new Vector2I(0, 0));
		}
	}

	private void RevealMap(Vector2 pos, int radius)
	{
		Vector2 localPos = ToLocal(pos);
		Vector2I center = LocalToMap(localPos);

		if (_lastRevealed == center)
		{
			return;
		}
		_lastRevealed = center;

		for (int y = -radius; y <= radius; y++)
		{
			for (int x = -radius; x <= radius; x++)
			{
				int manhattan = Mathf.Abs(x) + Mathf.Abs(y);
				if (manhattan > radius)
					continue;

				Vector2I cell = center + new Vector2I(x, y);

				EraseCell(cell);
			}
		}
	}

	private void Settings_FogToggle(bool toggle)
	{
		Visible = toggle;
	}
}
