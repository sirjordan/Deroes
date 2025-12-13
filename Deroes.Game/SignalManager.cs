using Godot;

public partial class SignalManager : Node
{
	private WindowManager _windowManager;

	[Signal] public delegate void Settings_WeatherToggleEventHandler(bool toggle);
	[Signal] public delegate void OpenWindowEventHandler(string scene);
	[Signal] public delegate void CloseWindowEventHandler(string scene);

	public static SignalManager Instance { get; private set; }

	public override void _EnterTree()
	{
		Instance = this;
		_windowManager = new WindowManager(GetTree().Root);
	}

	public override void _Ready()
	{
		Settings_WeatherToggle += SignalManager_SettingsWeatherToggle;
		OpenWindow += SignalManager_OpenWindow;
		CloseWindow += SignalManager_CloseWindow;
	}

	private void SignalManager_CloseWindow(string scene)
	{
		GD.Print($"SignalManager_CloseWindow: {scene}");
		_windowManager.Close(scene);
	}

	private void SignalManager_OpenWindow(string scene)
	{
		GD.Print($"SignalManager_OpenWindow: {scene}");
		_windowManager.Open(scene);
	}

	private void SignalManager_SettingsWeatherToggle(bool toggle)
	{
		GD.Print($"SignalManager_SettingsWeatherToggle: {toggle}");
	}
}
