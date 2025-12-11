using Godot;

public partial class CloseButton : Button
{
	[Export] public Control Context { get; set; }

	public override void _Pressed()
	{
		if (Context != null)
		{
			SignalManager.Instance.EmitSignal(SignalManager.SignalName.CloseWindow, Context.SceneFilePath);

			//Context.GetParent().SceneFilePath.QueueFree();
		}
		else
		{
			GD.PrintErr($"{nameof(Context)} must have a reference value");
		}
	}
}
