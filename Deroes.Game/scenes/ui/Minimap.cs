using Godot;

public partial class Minimap : TextureRect
{
	private Viewport _miniMapViewport;
	private Camera2D _minimapCamera;
	private CharacterBody2D _player;

	public override void _Ready()
	{
		// UI control where minimap appears
		_miniMapViewport = new SubViewport()
		{
			Size = new Vector2I((int)Size.X,(int)Size.Y),
			RenderTargetUpdateMode = SubViewport.UpdateMode.Always,
			RenderTargetClearMode = SubViewport.ClearMode.Always
		};
		AddChild(_miniMapViewport);

		_player = GetTree().Root.FindChild("Player", true, false) as CharacterBody2D;
		_minimapCamera = new Camera2D
		{
			Zoom = new Vector2(0.1f, 0.1f), 
			Position = _player.GlobalPosition,
			Enabled = true
		};
		_miniMapViewport.AddChild(_minimapCamera);

		// Duplicate tilemap into viewport
		var tilemap = GetTree().Root.FindChild("Ground", true, false);
		var _tilemapCopy = tilemap.Duplicate() as TileMapLayer;
		_tilemapCopy.ProcessMode = ProcessModeEnum.Disabled;
		_miniMapViewport.AddChild(_tilemapCopy);

		var scenery = GetTree().Root.FindChild("Scenery", true, false) as TileMapLayer;
		var sceneryCopy = scenery.Duplicate() as TileMapLayer;
		sceneryCopy.ProcessMode = ProcessModeEnum.Disabled;
		_miniMapViewport.AddChild(sceneryCopy);

		var fog = GetTree().Root.FindChild("Fog", true, false) as TileMapLayer;
		var fogCopy = fog.Duplicate() as TileMapLayer;
		fogCopy.ProcessMode = ProcessModeEnum.Disabled;
		_miniMapViewport.AddChild(fogCopy);

		Texture = _miniMapViewport.GetTexture();
		TextureFilter = TextureFilterEnum.NearestWithMipmaps;
		TextureRepeat = TextureRepeatEnum.Disabled;
	}

	public override void _Process(double delta)
	{
		_minimapCamera.GlobalPosition = _player.GlobalPosition;
	}
}
