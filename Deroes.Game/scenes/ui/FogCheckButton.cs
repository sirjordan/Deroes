using Godot;
using System;

public partial class FogCheckButton : CheckButton
{
	public override void _Toggled(bool toggledOn)
	{
		SignalManager.Instance.EmitSignal(SignalManager.SignalName.Settings_FogToggle, toggledOn);
	}
}
