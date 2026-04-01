using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class KuvaldaController2 : Node
{
    [Export] Player player;
    [Export] AnimationPlayer kuvaldaAnimationPlayer;
    public enum KuvaldaStates
    {
        Idle,
        Behind,
        Hit,
        Inspect,
        Returning,

    }
    public KuvaldaStates State = KuvaldaStates.Idle;
    public override void _PhysicsProcess(double delta)
    {
        switch(State)
        {
            case KuvaldaStates.Idle:
                Idle();
                break;
            case KuvaldaStates.Hit:
                Hit();
                break;
            case KuvaldaStates.Inspect:
                Inspect();
                break;
            case KuvaldaStates.Behind:
                Behind();
                break;
            case KuvaldaStates.Returning:
                Returning();
                break;
        }
    }
    public void Idle()
    {
        player.speedMultiplier = 1.0f;
        if (Input.IsActionJustPressed("m1"))
        {
            kuvaldaAnimationPlayer.Play("SwingBehind");
            State = KuvaldaStates.Behind;
        }
        if (Input.IsActionJustPressed("inspect"))
        {
            kuvaldaAnimationPlayer.Play("Inspect");
            kuvaldaAnimationPlayer.Queue("Base");
        }
    }
    public void Behind()
    {
        if (Input.IsActionJustReleased("m1"))
        {
            kuvaldaAnimationPlayer.Play("Hit");
            State = KuvaldaStates.Hit;
        }
        player.speedMultiplier = 0.4f;
    }
    public void Hit()
    {
        kuvaldaAnimationPlayer.Queue("Returning");
        State = KuvaldaStates.Returning;
    }
    public void Returning()
    {

    }
    public void Inspect()
    {

    }
    public void animationPlayerFinished(StringName name)
    {
        if (name == "Base")
        {
            State=KuvaldaStates.Idle;
        }
        if (name == "Returning")
        {
            State = KuvaldaStates.Idle;
        }
    }
} 
