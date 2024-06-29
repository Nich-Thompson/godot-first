using Godot;
using System;

public partial class Revolver : Node3D
{
	private double fireRate = 2;
	private double fireTimer = 0;
	private int ammo = 6;

	private AudioStreamPlayer shootSound;
	private AudioStreamPlayer reloadSound;
	private AudioStreamPlayer noAmmoSound;
	private AnimationPlayer animationPlayer;
	private RayCast3D rayCast;

	public override void _Ready()
	{
		shootSound = GetNode<AudioStreamPlayer>("ShootSound");
		reloadSound = GetNode<AudioStreamPlayer>("ReloadSound");
		noAmmoSound = GetNode<AudioStreamPlayer>("NoAmmoSound");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		rayCast = GetNode<Node3D>("RayHolder").GetChild<RayCast3D>(0);
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("click") && CanShoot())
			Shoot();
		else if (Input.IsActionJustPressed("reload") && ammo < 6)
			Reload();

		fireTimer -= delta;
	}

	private void Shoot()
	{
		if (ammo > 0)
		{
			ammo--;
			shootSound.Play();
			animationPlayer.Stop();
			animationPlayer.Play("Shoot");
			fireTimer = 1 / fireRate;

			if (rayCast.IsColliding())
			{
				// GD.Print("Hit: " + rayCast.GetCollider());
				var area = rayCast.GetCollider() as Area3D;
				if (area == null)
					return;

				GD.Print("ball");
				if (area.IsInGroup("enemy"))
				{
					GD.Print("Hit enemy");
					// charBody.TakeDamage(10);
				}
				if (area.IsInGroup("crit"))
				{
					GD.Print("Hit crit");
					// charBody.TakeDamage(20);
				}
			}
		}
		else
		{
			noAmmoSound.Play();
		}
	}

	private void Reload()
	{
		reloadSound.Play();
		ammo = 6;
		fireTimer = 1;
	}

	private bool CanShoot()
	{
		return fireTimer <= 0;
	}
}
