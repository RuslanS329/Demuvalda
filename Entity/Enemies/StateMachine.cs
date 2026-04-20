using Godot;
using System.Collections.Generic;
[GlobalClass]
public partial class StateMachine : State
{
    [Signal] public delegate void StateChangedEventHandler();

    //Children States Dictionary
    Godot.Collections.Dictionary<string, State> States = new();

    //State that sets at initialization
    //[Export] public State InitialState;
    [Export] bool debugStateChanges = false;
    //Current State
    [Export] public State CurrentState;


    public override void _Ready()
    {
        init();
    }
    public void getChildrenStates()
    {
        var children = GetChildren();
        foreach (var child in children)
        {
            if (child is State state)
            {
                state.init(this);
                if (States.ContainsKey(state.Name))
                {
                    States.Add(state.Name, state);
                }
                else
                {
                    States[state.StateName] = state;
                }
                GD.Print("Added state:" + state.Name);
            }
        }
    }
    public virtual void init()
    {
        getChildrenStates();
        //CurrentState = InitialState;
        CurrentState.Enter();
    }
    public override void _PhysicsProcess(double delta)
    {

        CurrentState.Do(delta);

    }
    public void ChangeState(string stateName)
    {
        if (debugStateChanges) 
        GD.Print("ChangeState : ", stateName);

        if (CurrentState != null) CurrentState.Exit();
        CurrentState = States[stateName];
        CurrentState.Enter();
        EmitSignal(SignalName.StateChanged);
    }
    public override void Do(double delta)
    {

    }
}
