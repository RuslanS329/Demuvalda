using Godot;
using System;
using System.Transactions;

public partial class HurtVignette : Sprite2D
{
    [Export] Health health;
    public float alpha = 0f;
    public override void _Ready()
    {
        Modulate = new (1.0f,1.0f,1.0f,0f);
        health.hit += play;
    }
    private void play(float damage)
    {
        Tween tween = GetTree().CreateTween();
        tween.SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(this, "modulate:a", 1f, 0.1);
        tween.TweenProperty(this, "modulate:a", 0f, 0.8);
    }
}
