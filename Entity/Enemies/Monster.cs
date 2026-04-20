using Godot;
using System;

public partial class Monster : Node
{
    [Export] public CharacterBody3D body;
    [Export] public NavigationAgent3D navAgent;
    [Export] float gravity = -10f;
    [Export] bool enableNavigation = true;
    [Export] bool debugNavigation = true;

    public Player player;
    public Vector3 playerPosition;
    public Vector3 nextPath;
    public Vector3 velocity;

    private int navigationUpdateCounter = 0;

    public override void _Ready()
    {
        player = GetTree().GetFirstNodeInGroup("Player") as Player;
    }
    public override void _PhysicsProcess(double delta)
    {
        playerPosition = player.GlobalPosition;
        if (enableNavigation)
        {
            navigation();
        }
        body.MoveAndSlide();
    }
    
    public void navigation()
    {
        
        navigationUpdateCounter++;
        if (navigationUpdateCounter > 30)
        {
            updateNavigation();

            if(debugNavigation) GD.Print("Nav Updated" + nextPath);

            navigationUpdateCounter = 0;
        }
    }
    public void move(Vector3 vel)
    {
        
        
        if (!body.IsOnFloor())
        velocity.Y += gravity * (float)GetPhysicsProcessDeltaTime();
        else
        {
            velocity.Y = 0;
        }
        velocity.X = vel.X;
        velocity.Z = vel.Z;
        body.Velocity = velocity;
        
    }
    public void updateNavigation()
    {
        if (navAgent != null && player != null)
        {
            
            navAgent.TargetPosition = player.GlobalPosition;
            nextPath = navAgent.GetNextPathPosition();

        }
    }
}
