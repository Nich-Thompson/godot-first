using Godot;
using System;

public partial class cube : MeshInstance3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Hello, World!");
		GD.Print(Position);
		Scale = new Vector3(5, 1, 1);
		// Mesh.SurfaceGetMaterial()l
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float speed = 10 * (float)delta;
		if (Input.IsActionPressed("ui_up") || Input.IsActionPressed("w"))
		{
			Position += new Vector3(0, 0, -speed);
		}
		if (Input.IsActionPressed("ui_down") || Input.IsActionPressed("s"))
		{
			Position += new Vector3(0, 0, speed);
		}
		if (Input.IsActionPressed("ui_left") || Input.IsActionPressed("a"))
		{
			Position += new Vector3(-speed, 0, 0);
		}
		if (Input.IsActionPressed("ui_right") || Input.IsActionPressed("d"))
		{
			Position += new Vector3(speed, 0, 0);
		}
	}
}
