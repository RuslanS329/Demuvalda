using Godot;
using System;

public partial class BackUp : State
{
    [Export] Monster monster;
    [Export] float speed = 2f;
    [Export] AnimationPlayer animationPlayer;
    [Export] string runAnimation;

    [Export] float backUpTime = 1f;
    
    public override void _Ready()
    {
        base._Ready();
        
    }
    public override void Enter()
    {
        GetTree().CreateTimer(backUpTime).Timeout += () =>
        {
            st.ChangeState("RunToPlayer");
        };
        animationPlayer.SpeedScale = -1;
    }
    public override void Exit()
    {
        animationPlayer.SpeedScale = 1;
    }

    public override void Do(double delta)
    {
        animationPlayer.Play(runAnimation);
        Vector3 direction = (monster.body.GlobalPosition - monster.playerPosition).Normalized();
        monster.move(direction * speed);
        //rotateToDirection(delta);
        

    }
    public void rotateToDirection(double delta)
    {
        Vector3 direction = (-monster.body.GlobalPosition + monster.nextPath).Normalized();
        Vector3 r = monster.body.GlobalRotation;
        r.Y = Mathf.LerpAngle(r.Y, Mathf.Atan2(-direction.X, -direction.Z), (float)delta * 4);
        monster.body.GlobalRotation = r;
    }
    
}
