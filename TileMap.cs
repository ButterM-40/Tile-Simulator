using Godot;
using System;
using System.Reflection;
public partial class TileMap : TileMapLayer
{
	[Export]
	public TileMapLayer tileMap; // Assign this in the inspector
	[Export]
	public TileSet tileSet;
	[Export]
	public int width = 10; // Width of the tile grid
	[Export]
	public int height = 10; // Height of the tile grid
	[Export]
	public int tileId = 0;
	public override void _Ready()
	{
		tileMap = GetNode<TileMapLayer>("%TileMapLayer");
		//GD.Print(tileMap.TileSet.TileCount);
		GD.Print(tileSet.GetType());
		MethodInfo[] methods = tileSet.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
		foreach (var method in methods)
		{
			GD.Print(method.Name);
		}
		GD.Print(tileSet.HasSource(0));
		GenerateTiles();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//GD.Print(GetGlobalMousePosition());
		//if (Input.IsActionJustPressed("mb_left")) {
			//GD.Print("True");
		//}
		//GD.Print("Tile Set Valid: " + String(tile_set.is_valid()));
		//Vector2I tile = LocalToMap(GetGlobalMousePosition());
		//SetCell(tile , 0);
		
	}
	
	public override void _PhysicsProcess(double delta)
	{
		// If we press the left mouse button, get the tile you're on and set it to 0 (Godot icon)
		if (Input.IsActionJustPressed("mb_left"))
		{
			Vector2I tile = LocalToMap(GetGlobalMousePosition());
			SetCell(tile, 0, Vector2I.Zero, 0);  // Set the tile to ID 00
		}

		// If the right button is pressed, remove the tile by setting it to -1
		if (Input.IsActionJustPressed("mb_right"))
		{
			Vector2I tile = LocalToMap(GetGlobalMousePosition());
			SetCell(tile, -1);  // Set the tile to ID -1 (no tile)
		}
	}
	private void GenerateTiles()
	{
		// Iterate through the grid and set tiles
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				// Use tile ID from your TileSet; here we use ID 0 for simplicity
				

				// Set the tile in the TileMap at the specified position
				SetCell(new Vector2I(x, y), 0, Vector2I.Zero, 0);
				//GD.Print($"Placed tile at ({x}, {y}) with ID {tileId}");
			}
		}
	}
}
