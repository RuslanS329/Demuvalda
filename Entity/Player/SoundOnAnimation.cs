using Godot;
using System;

public partial class SoundOnAnimation : AudioStreamPlayer
{
    [Export] AnimationPlayer player;
    [Export] string animationName;
    public override void _Ready()
    {
        player.AnimationStarted += play;
    }
    public void play(StringName name)
    {
        if(name == animationName)
        {
            Play();
        }
    }
}
