using Godot;
using System;

public partial class MenuActivator : Button
{
	ColorRect Menu;
	Button MenuDeactivator;
	Button CurrentButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Menu = GetNode<ColorRect>("%Menu");
		MenuDeactivator = GetNode<Button>("%MenuDeactivator");
		CurrentButton = GetNode<Button>("%MenuActivator");
	}

	public void _on_pressed(){
		GD.Print(MenuDeactivator.Visible);
		Menu.Visible = true;
		MenuDeactivator.Visible = true;
		CurrentButton.Visible = false;
	}
}
