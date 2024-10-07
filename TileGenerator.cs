using Godot;
using System;

public partial class TileGenerator : Node2D
{
	private PackedScene _Tile_Prefab;
	private const string TileName = "Tile_";
	private int currentStep = 0;


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
