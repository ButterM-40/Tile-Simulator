using Godot;
using System;

public partial class MenuDeactivator : Button
{
	ColorRect Menu;
	Button MenuActivator;
	Button CurrentButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Menu = GetNode<ColorRect>("%Menu");
		MenuActivator = GetNode<Button>("%MenuActivator");
		CurrentButton = GetNode<Button>("%MenuDeactivator");
	}

	public void _on_pressed(){
		Menu.Visible = false;
		MenuActivator.Visible = true;
		CurrentButton.Visible = false;
	}
}
