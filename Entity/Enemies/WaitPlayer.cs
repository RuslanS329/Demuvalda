using Godot;
using System;

public partial class WaitPlayer : State
{
    [Export] Area3D area;
    [Export] AnimationPlayer anim;
    bool detected = false;
    [Export] State RunState;

    public override void _Ready()
    {
        area.BodyEntered += bodyEntered;
        base._Ready();
    }
    public override void Enter()
    {

    }
    public override void Exit() 
    {
        
    }
    public override void Do(double delta)
    {

    }
    public void bodyEntered(Node3D body)
    {
        if(body is Player)
        {
            if (!detected)
            {
                anim.Play("Detected");
                anim.AnimationFinished += Finished;
                detected = true;
            }
            
            
        }
    }
    public void Finished(StringName name)
    {
        if (name == "Detected")
        {
            //st.ChangeState(RunState);
        }
    }
}
