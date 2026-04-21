using Godot;
using System;
using System.Linq.Expressions;

public partial class Footsteps : AudioStreamPlayer
{
    [Export] Player player;
    public override void _PhysicsProcess(double delta)
    {
        if (player.IsOnFloor())
        {
            if(player.inputDirection.LengthSquared() > 0)
            {
                if (!Playing)
                {
                    Play();
                }
                
                
            }
        }
        else
        {
            
        }
    }
}
