using Godot;

public partial class WeatherCheckButton : CheckButton
{
	public override void _Toggled(bool toggledOn)
	{
		SignalManager.Instance.EmitSignal(SignalManager.SignalName.Settings_WeatherToggle, toggledOn);
	}
}
