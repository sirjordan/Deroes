using Godot;
using System.Collections.Generic;

internal class WindowManager
{
	private Dictionary<string, Control> _openedWindows;
	private Window _root;

	public WindowManager(Window root)
	{
		_openedWindows = [];
		_root = root;
	}

	public void Open(string scene)
	{
		var loadedScene = GD.Load<PackedScene>(scene);
		var node = loadedScene.Instantiate<Control>();

		if (_openedWindows.TryAdd(scene, node))
		{
			node.Name = scene;
			_root.AddChild(node);
			node.Visible = true;
		}
		else
		{
			// Close if clicked again
			Close(scene);
		}
	}

	public void Close(string scene)
	{
		GD.Print(scene);
		if (_openedWindows.ContainsKey(scene))
		{
			_root.GetNode(scene).GetParent().QueueFree();
			_openedWindows.Remove(scene);
		}
	}
}
