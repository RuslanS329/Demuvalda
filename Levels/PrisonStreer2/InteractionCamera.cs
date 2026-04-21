using Godot;
using System;
using System.Security;
[GlobalClass]

public partial class InteractionCamera : Camera3D
{
    [Export] float time;
    [Export] Interactive interactive;
    public override void _Ready()
    {
        interactive.activated += start;
    }
    public void start()
    {
        Current = true;
        GetTree().CreateTimer(time).Timeout += () => { Current = false; };
    }
}
