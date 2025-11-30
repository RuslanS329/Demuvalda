using Godot;
using System;

public partial class govnokod : AnimationPlayer
{
    [Export] HitBox[] boxes;
    public override void _Ready()
    {
        foreach(var box in boxes)
        {
            box.sideHit += hit;
        }
    }
    public void hit(StringName name)
    {
        Play(name);  
    }
}
