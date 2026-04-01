using Godot;
using System.Collections.Generic;
[GlobalClass]
public partial class StateMachine : State
{
    [Signal] public delegate void StateChangedEventHandler();
   
    Godot.Collections.Dictionary<string,State> States;

    [Export] public State InitialState;
    

    public State CurrentState;

    
    public override void _Ready()
    {
        var children = GetChildren();
        foreach(var child in children)
        {
            if(child is State state)
            {
                state.init(this);
                States[state.StateName] = state;
            }
        }

   
        CurrentState = InitialState;
        init();
    }
    public virtual void init()
    {

    }
    public override void _PhysicsProcess(double delta)
    {
        
        CurrentState.Do(delta);
        
    }
    public void ChangeState(string stateName)
    {
        GD.Print("ChangeState : ", States[stateName]);

        if (CurrentState != null) CurrentState.Exit();
        CurrentState = States[stateName];
        CurrentState.Enter();
        EmitSignal(SignalName.StateChanged);
    }
    public override void Do(double delta)
    {

    }
}
