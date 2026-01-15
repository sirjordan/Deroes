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

	public void Open(string scene, string title = null, bool closeOthers = true)
	{
		var loadedScene = GD.Load<PackedScene>(scene);
		var node = loadedScene.Instantiate<Control>();

		if (!_openedWindows.ContainsKey(scene))
		{
			if (closeOthers)
			{
				foreach (var win in _openedWindows)
				{
					Close(win.Key);
				}
			}

			node.Name = scene;
			node.Visible = true;
			node.SetMeta("title", title ?? scene);

			_root.AddChild(node);
			_openedWindows.Add(scene, node);
		}
		else
		{
			// Close if clicked again
			Close(scene);
		}
	}

	public void Close(string scene)
	{
		if (_openedWindows.TryGetValue(scene, out Control control))
		{
			control.QueueFree();
			_openedWindows.Remove(scene);
		}
	}
}
