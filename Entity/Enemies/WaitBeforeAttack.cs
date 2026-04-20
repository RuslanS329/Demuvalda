using Godot;
using System;

public partial class WaitBeforeAttack : State
{
    [Export] float maxTime = 2f;
    [Export] float minTime = 0.6f;
    [Export] AnimationPlayer animationPlayer;
    [Export] string waitAnimation;
    [Export] Monster monster;
    
    
    public override void Enter()
    {
        animationPlayer.Play(waitAnimation);
        float rnd = (float)new Random().NextDouble();
        //rnd = Mathf.Lerp(minTime, maxTime, rnd);
        rnd = 1f;
        GetTree().CreateTimer(rnd).Timeout+=timeout;
        monster.body.Velocity = Vector3.Zero;
        monster.velocity = Vector3.Zero;
    }
    public void rotateToDirection(double delta)
    {
        Vector3 direction = (-monster.body.GlobalPosition + monster.playerPosition).Normalized();
        Vector3 r = monster.body.GlobalRotation;
        r.Y = Mathf.LerpAngle(r.Y, Mathf.Atan2(-direction.X, -direction.Z), (float)delta * 8);
        monster.body.GlobalRotation = r;
    }

    public override void Do(double delta)
    {
        rotateToDirection(delta);
    }
    public void timeout()
    {
        st.ChangeState("Attack");
    }
}
