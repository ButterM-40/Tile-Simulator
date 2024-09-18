using Godot;
using System;
using System.Collections.Generic;

public class Step_Change{
	public int Step_ID { get; set; }
	public string Status { get; set; }
}

public partial class TileManager : Node2D
{
	ColorRect TileColor;
	Color P = new Color(1.0f, 0.5f, 0.5f);
	Color W = new Color(0.56f, 0.93f, 0.56f);
	Color F = new Color(0.53f, 0.81f, 0.92f);
	
	public int SpawnStep {get; set;}
	public string SpawnColor {get; set;}
	
	List<Step_Change> ChangeModification = new List<Step_Change>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TileColor = GetNode<ColorRect>("%TileColor");
		
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
			//GD.PrintErr("Words");
		}else{
			GD.PrintErr("END ME");
		}
		//SpawnStep = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//TileColor.Color = W;
	}
	public void _Tile_Initialize(int first_seen){
		SpawnStep = first_seen;
	}
	public void _Tile_Modification_Change(int stage_id, string status){
		GD.Print("Tile modified with step: ", stage_id, " and status: ", status);
		ChangeModification.Add(new Step_Change() {Step_ID = stage_id, Status = status });
	}
	public List<Step_Change> returnTileList(){
		return ChangeModification;
	}
	public void tile_change(int current_step){
		GD.Print("Event Happening at Tile: " + this.Name + " " + current_step + " > "+ SpawnStep);
		if (current_step < SpawnStep){
			this.Visible = false;
		}else{
			this.Visible = true;
			if(current_step == SpawnStep){
				_change_color(SpawnColor);
			}else{
				GD.Print("Enter");
				int highest_step = SpawnStep;
				string highest_color = SpawnColor;
				foreach (Step_Change step in ChangeModification)
				{
					if (step.Step_ID <= current_step && step.Step_ID > highest_step){
						highest_step = step.Step_ID;
						highest_color = step.Status; 
					}
				}GD.Print("Enter" +SpawnColor );
				_change_color(highest_color);
			}
		}
	}
	public void _on_fileand_step_stage_changed(int current_step)
	{
		//GD.Print("Stage Changed Signal Received: " + current_step + " " +this.Name);
		tile_change(current_step);
	}
	public void _change_color(string Status){
		GD.Print( "STATUS:" + Status);
		switch (Status){
			case "W":
				TileColor.Color = W;
				break;
			case "P":
				TileColor.Color = P;
				break;
			case "F":
				TileColor.Color = F;
				break;
			default:
				TileColor.Color = new Color(1, 1, 1); // Default color
				break;
		}
	}
}
