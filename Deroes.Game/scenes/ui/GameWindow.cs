using Godot;

public partial class GameWindow : CanvasLayer
{
	public override void _Ready()
	{
		var title = GetNode<Label>("Wrapper/Title");
		title.Text = GetParent().GetMeta("title").AsString();
	}
}
