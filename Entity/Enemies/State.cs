using Godot;
using System;
[GlobalClass]
public partial class State : Node
{
    [Export] public string StateName = "";
    public StateMachine st;


    public override void _Ready()
    {
      
        base._Ready();
    }
    public virtual void init(StateMachine sm)
    {
        st = sm;
    }
    public virtual void Do(double delta)
    {
        
    }
    public virtual void Enter()
    {

    }
    public virtual void Exit() 
    { 

    }
}
