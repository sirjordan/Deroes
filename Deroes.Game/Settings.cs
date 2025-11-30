using Godot;
using System;

public partial class Settings : Button
{
	public override void _Pressed()
	{
		var settings = GetNode<Control>("../../../GameSettings");
		settings.Visible = true;
	}
}
