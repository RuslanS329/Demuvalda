using Godot;
using System;
[GlobalClass]
public partial class State : Node
{
    [Export] public string StateName = "";
    [Export] public StateMachine st;

    
    
    public virtual void init(StateMachine sm)
    {
        
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
