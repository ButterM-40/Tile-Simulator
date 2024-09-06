using Godot;
using System;
using System.IO;
public partial class HomeMenu : CanvasLayer
{
	
	string filename = "";
	Button Start;
	Button Browse;
	Label FileDisplay;
	FileDialog DialogDisplay;
	FileandStep FileandStep;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Start = GetNode<Button>("%StartTileButton");
		Browse = GetNode<Button>("%BrowseTileButton");
		FileDisplay = GetNode<Label>("%FileDisplayNameLabel");
		DialogDisplay = GetNode<FileDialog>("%FileExtraction");
		FileandStep = GetNode<FileandStep>("%FileandStep");
	}

	public void _on_browse_pressed(){
		DialogDisplay.Visible = true;
	}
	public void _on_file_extraction_file_selected(string path){
		filename = path;
		
		string[] filesplit = filename.Split(Path.DirectorySeparatorChar);
		Array.Reverse(filesplit);
		GD.Print(filesplit);
		FileDisplay.Text = filesplit[0];
		FileandStep._file_transfer(filename);
	}
	public void _on_start_tile_button_pressed(){
		FileandStep._start_tile_simulation();
	}
}
