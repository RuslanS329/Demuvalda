using DwarfImpulse;
using Godot;
using System;

public partial class CameraShakerPlayer : Node
{
    [Export] HurtBox hbox;
    [Export] ShakeDirector3D shakeDirector;
    [Export] private FastNoiseLite noise;

    public override void _Ready()
    {
        hbox.hit += shake;
    }
    private void shake()
    {

        NoiseShake ns = NoiseShake.CreateWithNoise(noise);
        shakeDirector.Shake(
             ns
           .WithDuration(0.2f)
           .WithEulersAmount(new Vector3(0.05f, 0, 0.03f)));
    }
}
