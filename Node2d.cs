using Godot;

public partial class MyCustomControl : Control
{
	public override void _Ready()
	{
		Update(); // This will trigger the _Draw method
	}

	// Custom drawing function
	public override void _Draw()
	{
		Vector2 position = new Vector2(100, 100); // Position where the rectangle is drawn
		Vector2 size = new Vector2(50, 50); // Size of the rectangle
		Color color = new Color(1, 0, 0); // Red color

		DrawRect(new Rect2(position, size), color);
	}

	public override void _Process(double delta)
	{
		Update(); // Call update to refresh the drawing
	}
}
