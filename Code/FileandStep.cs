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
	Node2D TileGeneration;
	int currentStep;
	string TileName = "Tile_ID_Controller_";

	[Signal]
	public delegate void StageChangedEventHandler(int current_step);

	LineEdit StepNumber;

	public override void _Ready()
	{
		currentStep = 0;
		filename = "";
		_Tile_Prefab = (PackedScene)GD.Load("res://Scenes/tile.tscn");
		//Remember to add case errors
		Root_Root = GetParent().GetParent();
		TileGeneration = Root_Root.GetNode<Node2D>("%TileGeneration");
		StepNumber = GetNode<LineEdit>("%StepNumber");
	}

	public void _file_transfer(string path)
	{
		filename = path;
	}

	public void _start_tile_simulation()
	{
		String json = System.IO.File.ReadAllText(filename);
		StageData deserialized = JsonConvert.DeserializeObject<StageData>(json);
		if (deserialized == null)
		{
			GD.PrintErr("Stage data is null!");
			return;
		}

		_convert_file_to_nodes(deserialized);
	}

	public void _convert_file_to_nodes(StageData deserialized)
	{
		foreach (var step in deserialized.Steps)
		{
			foreach (var tile in step.Tiles)
			{
				Node tileInstance = _Tile_Prefab.Instantiate();
				if (tileInstance is Node2D node2DInstance)
				{
					
					GD.PrintErr(TileGeneration.HasNode(TileName+tile.Tile_ID));
					//Node2D TileExist = TileGeneration.GetNode<Node2D>(TileName+tile.Tile_ID);;
					if (!TileGeneration.HasNode(TileName+tile.Tile_ID)) 
					{
						// Tile does not exist, so add it
						node2DInstance.Name = TileName + tile.Tile_ID;
						node2DInstance.Position = new Vector2(tile.X * 15, tile.Y * 15);
						
						node2DInstance.Set("SpawnStep", currentStep);
						node2DInstance.Set("SpawnColor", tile.Status);
						
						TileGeneration.AddChild(node2DInstance);
						GD.Print("Pass");
						GD.Print(currentStep);
					}
					else
					{
						// Tile exists, modify its status
						GD.Print("Do I ever Enter?");
						Node2D TileExist = TileGeneration.GetNode<Node2D>(TileName+tile.Tile_ID);
						TileExist.Call("_Tile_Modification_Change", tile.Tile_ID, tile.Status);
				   	 }
					}
			}
			currentStep += 1;
			GD.Print(currentStep);
		}
	}

	public void _on_step_number_text_submitted(string m)
	{
		var isNumeric = int.TryParse(StepNumber.Text, out int currentStep);
		if (isNumeric)
		{
			EmitSignal(nameof(StageChanged), currentStep);
		}
	}
}
