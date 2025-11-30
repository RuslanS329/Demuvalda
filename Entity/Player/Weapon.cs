using Godot;
using System;

public partial class Weapon : Node3D
{
    [Export] AnimationPlayer anim;
    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("attack"))
        {
            anim.Play("swing");
        }
    }
}
