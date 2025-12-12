using Godot;

public partial class Minimap : TextureRect
{
	private Viewport miniMapViewport;
	//private TextureRect miniMapRect;
	private TileMapLayer tilemapCopy;

	public override void _Ready()
	{
		// UI control where minimap appears
		//miniMapRect = GetNode<TextureRect>("UI/MiniMapRect");

		// Create viewport
		miniMapViewport = new SubViewport()
		{
			Size = new Vector2I(200, 200),
			RenderTargetUpdateMode = SubViewport.UpdateMode.Always,
			RenderTargetClearMode = SubViewport.ClearMode.Always,
			TransparentBg = true
		};
		AddChild(miniMapViewport);

		var minimapCamera = new Camera2D
		{
			Zoom = new Vector2(0.05f, 0.05f), // Increase this to zoom OUT
			Position = new Vector2(),// player.GlobalPosition,
			Enabled = true
		};
		miniMapViewport.AddChild(minimapCamera);

		// Duplicate tilemap into viewport
		var tilemap = GetTree().Root.FindChild("Ground", true, false);
		tilemapCopy = tilemap.Duplicate() as TileMapLayer;
		miniMapViewport.AddChild(tilemapCopy);

		Texture = miniMapViewport.GetTexture();
	}
}
