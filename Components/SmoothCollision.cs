using Godot;
using System;

public partial class SmoothCollision : Area3D
{
    [Export] Node3D Parent;
    [Export] public float Knockback = 0.5f;
    public SmoothCollision contact;
    public override void _Ready()
    {
        AreaEntered += areaEntered;
        AreaExited += areaExited;
    }

    

    public override void _PhysicsProcess(double delta)
    {
        if (contact != null)
        {
            Vector3 direction = (-contact.GlobalPosition + GlobalPosition).Normalized();
            direction.Y = 0;
            Parent.GlobalPosition += (direction * (float)delta * Knockback);
        }
    }
    public void areaEntered(Node3D area)
    {
        if(area is SmoothCollision)
        {
            contact = (SmoothCollision)area;
        }
    }
    public void areaExited(Node3D area)
    {
        if (area is SmoothCollision)
        {
            contact = null;
        }
    }
}
