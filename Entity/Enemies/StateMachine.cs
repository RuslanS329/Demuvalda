using Godot;
using System.Collections.Generic;
[GlobalClass]
public partial class StateMachine : State
{
    [Signal] public delegate void StateChangedEventHandler();
    //[Export] Godot.Collections.Array<Node> statesArray;
    [Export] public State InitialState;
    
    public Dictionary<string,State> states;

    public State CurrentState;

    
    public override void _Ready()
    {
        
        //foreach (State entry in statesArray)
        //{
        //    if (entry != null && !string.IsNullOrEmpty(entry.StateName))
        //    {
        //        states[entry.StateName] = entry;
        //        GD.Print($"Loaded state: {entry.StateName} -> {entry}");
        //    }
        //}
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
    public void ChangeState(State state)
    {
        GD.Print("ChangeState : ", state.StateName);

        if (CurrentState != null) CurrentState.Exit();
        CurrentState = state;
        CurrentState.Enter();
        //EmitSignal(SignalName.StateChanged);
    }
    public override void Do(double delta)
    {

    }
}
