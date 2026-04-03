using Godot;
using System;

public partial class PlayAnimationOnDeath : Node
{
    [Export] Health hp;
    [Export] AnimationPlayer player;
    [Export] string animationName;
    public override void _Ready()
    {
        hp.hit+= (float dmg)=>{ player.Play(animationName); };
    }
}
