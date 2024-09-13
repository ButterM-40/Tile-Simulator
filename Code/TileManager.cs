using Godot;
using System;

public partial class TileManager : Node2D
{
	ColorRect TileColor;
	Color P;
	Color W;
	Color F;
	
	int SpawnStep;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TileColor = GetNode<ColorRect>("%TileColor");
		P = new Color(.2f, .2f, 0f);
		W = new Color(.2f, .2f, 0f);
		F = new Color(.2f, .2f, 0f);
		
		Node Root_Root = GetParent();
		var NewStuff = Root_Root.GetNode<CanvasLayer>("%MainFileManager");
		var FileStep = NewStuff.GetNode<CanvasLayer>("%FileandStep");
		foreach (Node child in NewStuff.GetChildren())
		{
			GD.Print("Children's Name: ", child.Name);
		}	 
		if (FileStep != null)
		{
			var callable = new Callable(this, nameof(_on_fileand_step_stage_changed));
			FileStep.Connect("StageChanged", callable);
			GD.PrintErr("Words");
		}else{
			GD.PrintErr("END ME");
		}
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		TileColor.Color = W;
	}
	public void _Tile_Initialize(){
		
	}
	public void _Tile_Modification_Change(){
		
	}
	public void _on_fileand_step_stage_changed(int current_step)
	{
		GD.Print("Stage Changed Signal Received" + current_step);
	}
}
