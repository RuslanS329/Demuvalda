using Godot;
using System;
[GlobalClass]
public partial class ParticlesSpawner : Node3D
{
    [Export] public PackedScene Particles;
    public void Spawn()
    {
        var p = Particles.Instantiate() as Node3D;
        p.GlobalPosition = GlobalPosition;
        GetTree().Root.AddChild(p);
    }

}
