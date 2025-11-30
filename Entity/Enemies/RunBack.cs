using Godot;
using System;

public partial class RunBack : State
{
    [Export] float speed = 15f;
    [Export] Node3D body;
    [Export] AnimationPlayer anim;
    [Export] float max_time = 1.0f;
    [Export] State BiteState;
    [Export] State RunState;

    float time = 1.0f;

    public Player player;
    public override void _Ready()
    {
        player = GetTree().GetFirstNodeInGroup("Player") as Player;
        base._Ready();
    }

    public override void Enter()
    {
        anim.Play("Run");

        time = 0.8f + new Random().Next((int)max_time);
        GetTree().CreateTimer(time).Timeout+=timeout;
    }
    public override void Do(double delta)
    {

        Vector3 direction = (body.GlobalPosition - player.GlobalPosition).Normalized();

        body.GlobalPosition -= direction * speed * (float)delta * new Vector3(1, 0, 1);

        Vector3 r = body.GlobalRotation;


        r.Y = Mathf.LerpAngle(r.Y, Mathf.Atan2(-direction.X, -direction.Z), (float)delta * 4);
        body.GlobalRotation = r;
    }
    public void timeout()
    {
        int random = new Random().Next(1, 2);
        State action = random == 1 ? RunState : BiteState;
        st.ChangeState(action);
    }
    public override void Exit()
    {

    }

}
