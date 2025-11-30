using Godot;
using System;

public partial class Jump : State
{
    [Export] Node3D body;
    [Export] float jumpForce = 10f;
    [Export] AnimationPlayer anim;
    [Export] Falling fall;
    [Export] State RunState;
    float VelocityY = 0.0f;
    public override void _Ready()
    {
        anim.AnimationFinished += finished;
    }
    public override void Enter()
    {
        anim.Play("Jump");
        VelocityY = 40f;
    }
    public override void Do(double delta)
    {
        VelocityY -= 9.8f * (float)delta;
        
        body.Translate(VelocityY * Vector3.Up);
        if (fall.isOnFloor())
        {
            VelocityY = 0.0f;
        }
    }
    public void finished(StringName name)
    {
        if (name == "Jump")
        {
            anim.Play("Falling");
        }
    }
}
