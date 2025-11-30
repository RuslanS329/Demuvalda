using Godot;
using System;

public partial class FleshSpawner : Node3D
{
    [Export] PackedScene fl;
    [Export] Health hp;
    public override void _Ready()
    {
        base._Ready();
        hp.died += spawn;
    }
    public void spawn()
    {
        var f = fl.Instantiate() as Node3D;
        f.GlobalPosition = this.GlobalPosition;
        GetTree().Root.AddChild(f);
    }
}
