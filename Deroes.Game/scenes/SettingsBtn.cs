using Godot;
using System;

public partial class SettingsBtn : Button
{
	public override void _Pressed()
	{
		SignalManager.Instance.EmitSignal(SignalManager.SignalName.OpenWindow, "res://scenes/gameSettings.tscn", "Main Menu");
	}
}
