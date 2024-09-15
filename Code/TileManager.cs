using Godot;
using System;
using System.Collections.Generic;

public class Step_Change{
	public int Step_ID { get; set; }
	public char Status { get; set; }
}

public partial class TileManager : Node2D
{
	ColorRect TileColor;
	Color P;
	Color W;
	Color F;
	
	int SpawnStep;
	List<Step_Change> ChangeModification;
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
		SpawnStep = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		TileColor.Color = W;
	}
	public void _Tile_Initialize(int first_seen){
		SpawnStep = first_seen;
	}
	public void _Tile_Modification_Change(int stage_id, char status){
		ChangeModification.Add(new Step_Change() {Step_ID = stage_id, Status = status });
	}
	public List<Step_Change> returnTileList(){
		return ChangeModification;
	}
	public void tile_change(int current_step){
		if (current_step < SpawnStep){
			this.Visible = true;
			Step_Change modification = ChangeModification.Find(x => x.Step_ID == current_step);
			if(modification.Status == 'W'){
				TileColor.Color = W;
			}
			else if (modification.Status == 'P'){
				TileColor.Color = P;
			}
			else{
				TileColor.Color = F;
			}
		}
		else if (current_step > SpawnStep){
			this.Visible = false;
		}
	}
	public void _on_fileand_step_stage_changed(int current_step)
	{
		GD.Print("Stage Changed Signal Received: " + current_step + this.Name);
		tile_change(current_step);
	}
}
