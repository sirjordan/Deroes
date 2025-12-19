using Godot;

public partial class Fog : TileMapLayer
{
	[Export] public int TileId { get; set; } = 1;

	private TileMapLayer _terrain;

	public override void _Ready()
	{
		SignalManager.Instance.Settings_FogToggle += Settings_FogToggle;

		// Cover all terrain tiles with Fog
		_terrain = GetNode<TileMapLayer>("../Ground");
		
		foreach (var vcell in _terrain.GetUsedCells())
		{
			SetCell(vcell, TileId, new Vector2I(0, 0));
		}
	}

	public void Reveal(Vector2 pos, int radius)
	{
		var center = LocalToMap(pos);

		for (int y = -radius; y <= radius; y++)
		{
			for (int x = -radius; x <= radius; x++)
			{
				if (Mathf.Abs(x) + Mathf.Abs(y) <= radius)
				{
					var cell = center + new Vector2I(x, y);
					EraseCell(cell);
				}
			}
		}
	}

	private void Settings_FogToggle(bool toggle)
	{
		Visible = toggle;
	}
}
