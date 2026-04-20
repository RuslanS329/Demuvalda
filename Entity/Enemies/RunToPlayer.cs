using Godot;
using System;

public partial class RunToPlayer : State
{
    [Export] Monster monster;
    [Export] float speed = 5f;
    [Export] AnimationPlayer animationPlayer;
    [Export] string runAnimation;
    public override void _Ready()
    {
        base._Ready();
        
    }


    public override void Do(double delta)
    {
        animationPlayer.Play(runAnimation);
        Vector3 direction = (-monster.nextPath + monster.playerPosition).Normalized();
        monster.move(direction * speed);
        rotateToDirection(delta);
        if(monster.body.GlobalPosition.DistanceTo(monster.playerPosition) < 4)
        {
            reachedPlayer();
        }
        
    }
    public void rotateToDirection(double delta)
    {
        Vector3 direction = (monster.body.GlobalPosition - monster.nextPath).Normalized();
        Vector3 r = monster.body.GlobalRotation;
        r.Y = Mathf.LerpAngle(r.Y, Mathf.Atan2(-direction.X, -direction.Z), (float)delta * 8);
        monster.body.GlobalRotation = r;
    }
    public void reachedPlayer()
    {
        st.ChangeState("WaitBeforeAttack");
    }
}

