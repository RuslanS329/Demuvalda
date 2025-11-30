using Godot;
using System;

public partial class Run : State
{
    [Export] State BiteState;

    [Export] float speed = 15f;
    [Export] Node3D body;
    [Export] AnimationPlayer anim;
    [Export] Area3D AttackArea;
    public Player player;
    public override void _Ready()
    {
        player = GetTree().GetFirstNodeInGroup("Player") as Player;
        AttackArea.BodyEntered += bodyEntered;
        base._Ready();
    }
    public override void Enter()
    {
        anim.Play("Run");
    }
    public override void Do(double delta)
    {
        Vector3 direction = (-body.GlobalPosition + player.GlobalPosition).Normalized();
        
        body.GlobalPosition += direction * speed * (float)delta * new Vector3(1,0,1);


        //Vector2 pos2d = new Vector2(body.GlobalRotation.X, body.GlobalRotation.Z);
        //Vector2 playerpos2d = new Vector2(player.GlobalRotation.X, player.GlobalRotation.Z);
        //Vector2 dir = -(pos2d - playerpos2d);
        //body.GlobalRotation = new Vector3(r.X, Mathf.LerpAngle(r.Y,Mathf.Atan2(dir.X,dir.Y),(float)delta * 2), r.Z);

        rotateToPlayer(delta);
    }
    public void rotateToPlayer(double delta)
    {
        Vector3 direction = (-body.GlobalPosition + player.GlobalPosition).Normalized();
        Vector3 r = body.GlobalRotation;
        r.Y = Mathf.LerpAngle(r.Y, Mathf.Atan2(-direction.X, -direction.Z), (float)delta * 4);
        body.GlobalRotation = r;
    }
    public override void Exit()
    {

    }
    public void bodyEntered(Node body)
    {
        if(body is Player)
        {
            
            st.ChangeState(BiteState);

        }
    }
}
