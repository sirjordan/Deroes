using Godot;

public partial class SignalManager : Node
{
	private WindowManager _windowManager;

	[Signal] public delegate void Settings_WeatherToggleEventHandler(bool toggle);
	[Signal] public delegate void Settings_FogToggleEventHandler(bool toggle);

	[Signal] public delegate void OpenWindowEventHandler(string scene, string title);
	[Signal] public delegate void CloseWindowEventHandler(string scene);

	[Signal] public delegate void RevealMapEventHandler(Vector2 position, int radius);

	public static SignalManager Instance { get; private set; }

	public override void _EnterTree()
	{
		Instance = this;
		_windowManager = new WindowManager(GetTree().Root);
	}

	public override void _Ready()
	{
		OpenWindow += (string scene, string title) => _windowManager.Open(scene, title);
		CloseWindow += _windowManager.Close;
	}
}
