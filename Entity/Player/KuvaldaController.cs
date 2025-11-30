using Godot;
using System;
using System.Security;
using System.Security.Cryptography.X509Certificates;

public partial class KuvaldaController : Node
{
    [Export] Player player;
    [Export] AnimationPlayer animationPlayer;
    [Export] ChainGenerator chainGenerator;
    [Export] Marker3D KuvaldaProjectileSpawnPoint;
    [Export] Node3D KuvaldaMesh;
    [Export] RayCast3D KuvaldaRayCast;
    [Export] PackedScene KuvaldaProjectile;
    [Export] Node3D Player;
    [Export] Node3D chainEnd;
    [Export] HurtBox hurtBox;
    Kuvalda kuv;

    [Signal] public delegate void StateChangedEventHandler(int state);
    [Signal] public delegate void AttackEventHandler(string name);
    public bool hit_left = true;

    public float max_speed = 8.0f;
    public float speed_boost = 0.5f;
    public float current_speed = 1.0f;
    public enum States
    {
        Idle,
        Attack,
        Return,
        SpinStart,
        SpinLoop,
        SpinEnd,
        Thrown,
    }
    public States state = States.Idle;
    public override void _Ready()
    {
        base._Ready();

        animationPlayer.AnimationFinished += AnimationFinished;

    }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        switch (state)
        {
            case States.Idle:
                IdleState();
                break;
            case States.SpinLoop:
                SpinLoopState(delta);
                break;
            case States.Thrown:
                ThrownState();
                break;
        }

    }
    public void ThrownState()
    {

    }
    public void IdleState()
    {
        if (Input.IsActionJustPressed("m1"))
        {
            if (hit_left)
            {
                animationPlayer.Play("RightBaked");
            }
            else
            {
                animationPlayer.Play("LeftBaked");
            }
            startHurtBoxHurtTimer(0.4f);
            state = States.Attack;
            hit_left = !hit_left;
            hurtBox.ignoreList.Clear();
            hurtBox.hurt = true;
        }
        if (Input.IsActionJustPressed("m2"))
        {
            animationPlayer.Play("SpinStartBaked");
            state = States.SpinLoop;
        }
    }
    public void startHurtBoxHurtTimer(float time)
    {
        GetTree().CreateTimer(time).Timeout += hurtBoxHurtTimeOut;
    }
    public void hurtBoxHurtTimeOut()
    {
        hurtBox.hurt = false;
    }
    public void SpinLoopState(double delta)
    {


        if (Input.IsActionPressed("m2"))
        {
            if (!animationPlayer.IsPlaying())
            {
                animationPlayer.Play("LoopBaked");
            }
        }
        current_speed += speed_boost * (float)delta;
        current_speed = Mathf.Clamp(current_speed, 1.0f, max_speed);
        animationPlayer.SpeedScale = current_speed;

    }
    public void boost()
    {
        if (!player.IsOnFloor())
        {
            player.Velocity = kuv.direction * 20f;
            player.boost = kuv.direction * 10f;

        }
    }
    public void AnimationFinished(StringName name)
    {
        hurtBox.hurt = false;
        if (name == "LoopBaked" || name == "SpinStartBaked")
        {
            if (!Input.IsActionPressed("m2"))
            {
                KuvaldaMesh.Visible = false;
                state = States.Thrown;
                SpawnKuvaldaProjectile(current_speed);
                animationPlayer.SpeedScale = 1.0f;
                boost();
            }
            else
            {
                animationPlayer.Play("LoopBaked");
            }
        }
        if (name == "LeftBaked" || name == "RightBaked")
        {
            animationPlayer.Queue("Baza");
            state = States.Idle;
        }


    }
    public void KuvaldaReturned()
    {
        state = States.Idle;
        kuv.QueueFree();
        KuvaldaMesh.Visible = true;
        animationPlayer.Play("Baza");
        chainGenerator.ChainEnd = chainEnd;
    }
    public void SpawnKuvaldaProjectile(float speed)
    {
        kuv = KuvaldaProjectile.Instantiate() as Kuvalda;
        kuv.player = Player;
        kuv.speed += speed;
        kuv.direction = (-KuvaldaRayCast.GlobalPosition + KuvaldaProjectileSpawnPoint.GlobalPosition).Normalized();
        kuv.rotat = KuvaldaRayCast.GlobalRotation;
        kuv.GlobalPosition = KuvaldaProjectileSpawnPoint.GlobalPosition;

        chainGenerator.ChainEnd = kuv;
        GetTree().Root.AddChild(kuv);
        current_speed = 1.0f;
    }
}
