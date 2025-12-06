using Godot;

public partial class SignalManager : Node
{
	[Signal] public delegate void SettingsWeatherToggleEventHandler(bool toggle);

	public static SignalManager Instance { get; private set; }

	public override void _EnterTree()
	{
		Instance = this; 
	}

	public override void _Ready()
	{
		SettingsWeatherToggle += SignalManager_SettingsWeatherToggle;
	}

	private void SignalManager_SettingsWeatherToggle(bool toggle)
	{
		// Other things we want to have as middleware
	}
}
