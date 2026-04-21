using Godot;
using System;

public partial class PrisonStreetBridge : MeshInstance3D
{
    [Export] Interactive interactive;
    public override void _Ready()
    {
        interactive.activated += activate;
    }

    public void activate()
    {

        var tween = GetTree().CreateTween();
        tween.SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(this, "rotation", new Vector3(0f,0f,0f), 2.0f);
    }
}
