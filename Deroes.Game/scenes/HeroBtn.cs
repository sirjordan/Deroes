using Godot;

public partial class HeroBtn : Button
{
	public override void _Pressed()
	{
		SignalManager.Instance.EmitSignal(SignalManager.SignalName.OpenWindow, "res://scenes/inventory.tscn");
	}
}
