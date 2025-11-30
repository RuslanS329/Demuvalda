using Godot;
using System;

public partial class Hands : Node3D
{
    [Export] Node3D camera;
    [Export] float max_rotation = .5f;
    float rot_x;
    
    public override void _PhysicsProcess(double delta)
    {
        
        Vector3 rot = RotationDegrees;
        RotationDegrees = new (camera.RotationDegrees.X,rot.Y,rot.Z);

    }
}
