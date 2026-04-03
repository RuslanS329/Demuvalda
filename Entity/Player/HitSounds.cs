using Godot;
using System;

public partial class HitSounds : Node
{
    [Export] Godot.Collections.Array<AudioStreamPlayer> audios;
    [Export] HurtBox hbox;
    public override void _Ready()
    {
        hbox.hit += playRandom;
    }
    void playRandom()
    {
        audios.PickRandom().Play();
    }
}
