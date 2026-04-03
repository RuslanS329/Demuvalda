using Godot;
using System;

public partial class BloodParticleSpawner : Node3D
{
    [Export] PackedScene ptc;
    [Export] HurtBox hbox;
    public override void _Ready()
    {
        hbox.hit += spawn;
    }
    public void spawn()
    {
        GpuParticles3D node = ptc.Instantiate() as GpuParticles3D;
        GetTree().Root.AddChild(node);
        node.GlobalPosition = GlobalPosition;
        node.Emitting = true;
    }
}
