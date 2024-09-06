using Godot;
using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Tile
{
	public int x { get; set; }
	public int y { get; set; }
	public string Status { get; set; }
}

public class Stage {
	public string StageNumber { get; set; }
	public List<Tile> Tiles { get; set; }
}
public class StageData
{
	public List<Stage> Stages { get; set; }
}
public partial class FileandStep : CanvasLayer
{
	string filename = "";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		filename = "";
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
		GD.Print(deserialized);
	}
	public void _convert_file_to_nodes(Stage JSONFile){
		
	}
	public void _switch_stage(int stage_id){
		
	}
}
