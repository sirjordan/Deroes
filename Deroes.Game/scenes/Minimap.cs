using Godot;

public partial class Minimap : TextureRect
{
	private Viewport _miniMapViewport;
	private TileMapLayer _tilemapCopy;
	private Camera2D _minimapCamera;
	private CharacterBody2D _player;

	public override void _Ready()
	{
		// UI control where minimap appears
		_miniMapViewport = new SubViewport()
		{
			Size = new Vector2I((int)Size.X, (int)Size.Y), 
			RenderTargetUpdateMode = SubViewport.UpdateMode.Always,
			RenderTargetClearMode = SubViewport.ClearMode.Always,
			//TransparentBg = true
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
		_tilemapCopy = tilemap.Duplicate() as TileMapLayer;
		_miniMapViewport.AddChild(_tilemapCopy);

		Texture = _miniMapViewport.GetTexture();
	}

	public override void _Process(double delta)
	{
		_minimapCamera.GlobalPosition = _player.GlobalPosition;
	}

	public override void _Draw()
	{
		base._Draw();
		//DrawCircle(_player.GlobalPosition, 25, Colors.DarkRed, filled: true);
	}
}
