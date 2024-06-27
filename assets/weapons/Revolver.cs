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
	private Node3D rayHolder;

	public override void _Ready()
	{
		shootSound = GetNode<AudioStreamPlayer>("ShootSound");
		reloadSound = GetNode<AudioStreamPlayer>("ReloadSound");
		noAmmoSound = GetNode<AudioStreamPlayer>("NoAmmoSound");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		rayHolder = GetNode<Node3D>("RayHolder");
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
