using Godot;
using System;

public partial class HpBar : ProgressBar
{
    [Export] Health health;
    [Export] float weight = 0.1f;
    private float target_value;
    private float current_value;
    public override void _Ready()
    {
        current_value = health.health;
    }
    public override void _PhysicsProcess(double delta)
    {
        target_value = health.health;
        current_value = Mathf.Lerp(current_value,target_value,weight);
        Value = current_value;
    }
}
