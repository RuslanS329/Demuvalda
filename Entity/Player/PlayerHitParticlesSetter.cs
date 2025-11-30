using Godot;
using System;

public partial class PlayerHitParticlesSetter : Node
{
    [Export] ParticlesSpawner ps;
    [Export] HurtBox hb;
    public override void _Ready()
    {
        hb.hit += ps.Spawn;
    }
}
