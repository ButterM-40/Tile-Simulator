using Godot;
using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Tile
{
	public string Tile_ID {get; set;}
	public int X { get; set; }
	public int Y { get; set; }
	public string Status { get; set; }
}

public class Step {
	public string StepNumber { get; set; }
	public List<Tile> Tiles { get; set; }
}
public class StageData
{
	public List<Step> Steps { get; set; }
}
public partial class FileandStep : CanvasLayer
{
	private PackedScene _Tile_Prefab;
	string filename = "";
	Node2D Scene_Stage_Steps;
	Node Root_Root;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		filename = "";
		_Tile_Prefab = (PackedScene)GD.Load("res://Scenes/tile.tscn");
		Root_Root = GetParent().GetParent();
		GD.Print(GetParent().GetParent());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void _file_transfer(string path)
	{
		filename = path;
		GD.Print("String" + filename);
	}
	public void _start_tile_simulation(){
		GD.Print("String2 " + filename);
		String json = System.IO.File.ReadAllText(filename);
		GD.Print(json);
		StageData deserialized = JsonConvert.DeserializeObject<StageData>(json);
		if (deserialized == null)
		{
				GD.PrintErr("Stage data is null!");
				return; // Stop execution
		}
		// Print the count of stages
		GD.Print(deserialized.Steps);
		_convert_file_to_nodes(deserialized);
	}
	public void _convert_file_to_nodes(StageData deserialized){
		//Node instance = _Tile_Prefab.Instance();
		//AddChild(instance);
		foreach (var step in deserialized.Steps)
		{
			// Access the Step number
			GD.Print("Step Number: " + step.StepNumber);

			// Loop through each Tile in the Step
			foreach (var tile in step.Tiles)
			{
				// Access Tile information
				GD.Print("Tile ID: " + tile.Tile_ID);
				GD.Print("Tile Position: (" + tile.X + ", " + tile.Y + ")");
				GD.Print("Tile Status: " + tile.Status);
				Node tileInstance = _Tile_Prefab.Instantiate();
		
			// Check if the instance is of type Node2D and cast it
				if (tileInstance is Node2D node2DInstance)
				{
					node2DInstance.Name = tile.Tile_ID;
					node2DInstance.Position = new Vector2(tile.X * 15, tile.Y * 15);
					GD.Print("Work");
					Root_Root.AddChild(node2DInstance);
				}
				
			}
		}
	}
	public void _switch_stage(int stage_id){
		
	}
	public (int new_x, int new_y) _x_y_converter(int x, int y){
		return (x * 15, y * 15);
	}
}
