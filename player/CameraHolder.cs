using Godot;
using System;

public partial class CameraHolder : Node3D
{
	private const float Sensitivity = 0.04f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

    public override void _Input(InputEvent @event)
    {
		if (@event is InputEventMouseMotion mouseEvent)
		{
			RotateX(Mathf.DegToRad(-mouseEvent.Relative.Y * Sensitivity));
			
		}
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
