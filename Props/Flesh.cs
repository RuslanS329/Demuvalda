using Godot;
using System;

public partial class Flesh : Area3D
{
    public bool can = false;
    Player pl;
    public override void _Ready()
    {
        BodyEntered += body;
        BodyExited += bodyEx;
    }
    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("e"))
        {
            
            //Node h = pl.GetNode("Health");
            //if(h is Health hp)
            //{
            //    hp.health += 20f;
            //}
            QueueFree();
        }
    }
    public void body(Node3D body)
    {
        if (body is Player b)
        {
            pl = b;
            can = true;
        }
    }
    public void bodyEx(Node3D body)
    {
        if (body is Player b)
        {
            pl = b;
            can = false;
        }
    }
}
