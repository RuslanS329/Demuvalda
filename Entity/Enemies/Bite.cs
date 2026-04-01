using Godot;
using System;

public partial class Bite : State
{
    [Export] float damage = 10f;
    [Export] float random_damage = 10f;
    [Export] AnimationPlayer anim;
    [Export] Run RunState;
    [Export] State RunBack;
    [Export] State JumpState;
    [Export] HurtBox hurtBox;
    bool full_bite = false;
    bool rotate = true;
    public override void _Ready()
    {
        anim.AnimationFinished += finished;
        base._Ready();
    }
    public override void Enter()
    {
        anim.PlaySection("Bite",0.0,0.22);
        double time = 0.22 + new Random().NextDouble() * 0.2;
        GetTree().CreateTimer(time).Timeout+=play;
       
    }
    public void play()
    {
        anim.PlaySection("Bite", 0.22, 0.8333);
        hurtBox.hurt = true;
        full_bite = true;
    }
    public override void Exit()
    {

    }
    public override void Do(double delta)
    {
        if (rotate)
        {
            RunState.rotateToPlayer(delta);
        }
        base.Do(delta);

    }
    public void finished(StringName name)
    {
        
        if(name == "Bite" && full_bite)
        {
            full_bite = false;
            double chance;
            
            
            chance = new Random().NextDouble();
           
            //st.ChangeState(RunState);
            hurtBox.hurt = false;
            hurtBox.ignoreList.Clear();
            

        }
    }
}
