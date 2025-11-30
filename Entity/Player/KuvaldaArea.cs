using Godot;
using System;

public partial class KuvaldaArea : Area3D
{
    [Export] KuvaldaController kc;
    public override void _Ready()
    {
        AreaEntered += areaEntered;
    }
    public void areaEntered(Node3D area)
    {
        GD.Print("Area entered");
        if(area is Kuvalda)
        {
            kc.KuvaldaReturned();
        }
    }
}
