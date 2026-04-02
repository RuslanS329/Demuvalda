using Godot;
using System;

public partial class HurtSounds : Node
{
    [Export] Godot.Collections.Array<AudioStreamPlayer> audios;
    [Export] Health health;
    public override void _Ready()
    {
        health.hit += playRandom;
    }
    void playRandom(float damage)
    {
        audios.PickRandom().Play();
    }
}
