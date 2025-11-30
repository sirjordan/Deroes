using Godot;
using System;

public partial class CloseButton : Button
{
	public override void _Pressed()
	{
		GetParentControl().GetParentControl().Visible = false;
	}
}
