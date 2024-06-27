using Godot;
using System;

public partial class NecoBall : CharacterBody3D
{
	public const float Speed = 1.5f;
	public const float JumpVelocity = 4.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	private enum State
	{
		Idle,
		Tracking
	}

	private State state = State.Idle;
	private CharacterBody3D player;
	private NavigationAgent3D navigationAgent;

    public override void _Ready()
    {
		Area3D area = GetNode<Area3D>("Area3D");
		area.BodyEntered += OnBodyEntered;
		area.BodyExited += OnBodyExited;

		player = (CharacterBody3D)GetTree().GetFirstNodeInGroup("Player");
		navigationAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
    }

	private void OnBodyEntered(Node3D body)
    {
        GD.Print("Body entered: ", body.Name);
		if (body.IsInGroup("Player"))
		{
			GD.Print("Player entered");
			state = State.Tracking;
		}
    }

	private void OnBodyExited(Node3D body)
	{
		GD.Print("Body exited: ", body.Name);
		if (body.IsInGroup("Player"))
		{
			GD.Print("Player entered");
			state = State.Idle;
		}
	}

    public override void _PhysicsProcess(double delta)
	{
		switch (state)
		{
			case State.Idle:
				// Idle(delta);
				break;
			case State.Tracking:
				Chase(delta);
				break;
		}

		// Vector3 velocity = Velocity;

		// // Add the gravity.
		// if (!IsOnFloor())
		// 	velocity.Y -= gravity * (float)delta;

		// Velocity = velocity;
		// MoveAndSlide();
		MoveAndCollide(Velocity);
	}

	private void Chase(double delta)
	{
		Velocity = (navigationAgent.GetNextPathPosition() - Position).Normalized() * Speed * (float)delta;

		if (player.Position.DistanceTo(Position) > 1)
		{
			navigationAgent.TargetPosition = player.Position;
			// MoveAndCollide(Velocity);
		}
	}
}
