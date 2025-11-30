using Godot;
using Microsoft.VisualBasic.FileIO;
using System;

public partial class Kuvalda : Area3D
{
    [Export] ParticlesSpawner ps;
    [Export] ParticlesSpawner ps2;
    public Node3D player;
    public Vector3 rotat;
    public Vector3 direction;
    public float speed = 30f;
    bool returning = false;
    public float max_time = 0.6f;
    public float time = 0.0f;
    [Export] HurtBox hb;
    public override void _Ready()
    {
        GlobalRotation = rotat;
        BodyEntered += bodyEntered;
        hb.hit += ps2.Spawn;
    }
    public override void _PhysicsProcess(double delta)
    {
        time += (float)delta;
        if (time > max_time) returning = true;
        if (!returning)
        {
            Vector3 offset = new Vector3(0,0,-10);
            Position += direction * (float)delta * speed;
            GlobalRotation = rotat;
        }
        else
        {
            Vector3 dir = (player.GlobalPosition - GlobalPosition + new Vector3(0f,1.0f,0f)).Normalized();
            Position += dir * (float)delta * speed;
        }
    }
    public void bodyEntered(Node3D body)
    {
        returning = true;
        ps.Spawn();
    }
}
