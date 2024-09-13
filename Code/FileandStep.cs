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

	int currentStep;

	[Signal]
	public delegate void StageChangedEventHandler(int current_step);

	LineEdit StepNumber;

	public override void _Ready()
	{
		currentStep = 0;
		filename = "";
		_Tile_Prefab = (PackedScene)GD.Load("res://Scenes/tile.tscn");
		Root_Root = GetParent().GetParent();
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
					node2DInstance.Name = tile.Tile_ID;
					node2DInstance.Position = new Vector2(tile.X * 15, tile.Y * 15);
					Root_Root.AddChild(node2DInstance);
				}
			}
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
