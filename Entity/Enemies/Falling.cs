using Godot;
using System;

public partial class Falling : Node
{
    Node3D Body;
    [Export] float speed = 9.8f;
    [Export] RayCast3D FloorRayCast;
    bool fall = true;

    public override void _Ready()
    {
        Body = GetParent() as Node3D;
    }
    public override void _PhysicsProcess(double delta)
    {
        if (!FloorRayCast.IsColliding())
        {
            Body.Translate(speed * (float)delta * Vector3.Down);
        }
        else
        {
            Vector3 Distance = -Body.GlobalPosition + FloorRayCast.GetCollisionPoint();
            Body.GlobalPosition += Vector3.Up * Distance;
        }

    }
    public bool isOnFloor()
    {
        if(FloorRayCast.IsColliding())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
