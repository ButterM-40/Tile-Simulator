using Godot;

public partial class ZoomAndMoveCamera : Camera2D
{

	private bool isDragging = false;
	private Vector2 dragStartPosition = Vector2.Zero;
	private Vector2 cameraStartPosition = Vector2.Zero;
	private float dragSpeedMultiplier = 1.0f;

	public override void _Input(InputEvent @event)
	{

		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.Pressed)
			{
				if (mouseEvent.ButtonIndex == MouseButton.WheelUp || Input.IsActionPressed("Mouse_Up")){
					if (Zoom.X < 10f ){
						Zoom += new Vector2(0.25f, 0.25f);
						//dragSpeedMultiplier *= 0.3f;
						//GD.Print(Zoom);
					}
				}
				else if (mouseEvent.ButtonIndex == MouseButton.WheelDown || Input.IsActionPressed("Mouse_Down")){
					if (Zoom.X-.25f != 0 ){
						Zoom -= new Vector2(0.25f, 0.25f);
						//dragSpeedMultiplier *= 0.3f;
						//GD.Print(Zoom);
					}
				}
				else if (mouseEvent.ButtonIndex == MouseButton.Left){
					isDragging = true;
					dragStartPosition = mouseEvent.Position;
					cameraStartPosition = GlobalPosition;
				}
			}
			else{
				isDragging = false;
			}
		}
		else if (@event is InputEventMouseMotion motionEvent && isDragging){
			Vector2 dragOffset = (dragStartPosition - motionEvent.Position) * dragSpeedMultiplier;
			GlobalPosition = cameraStartPosition + dragOffset;
		}
	}
}
