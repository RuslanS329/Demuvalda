using Godot;
using System;

public partial class PlayerUi : Control
{
    [Export] TextureRect tr;
    [Export] TextureRect tr2;
    public override void _PhysicsProcess(double delta)
    {
        
    }
    public void SetHp(float value)
    {

        value /= 100.0f;
        //0-1 SCREEN UV BASICALLY

        value *= 0.4f;
        value -= 0.1f;
        // 30-604 SO CORRECT
        
        var material1 = tr.Material as ShaderMaterial;
        var material2 = tr2.Material as ShaderMaterial;

        material1.SetShaderParameter("disappearX", value);
        material2.SetShaderParameter("disappearX", value);
    }
}
