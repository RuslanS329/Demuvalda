using Godot;
using System;

public partial class DvuNogSM : StateMachine
{
 
    [Export] Label debugl;
    [Export] HitBox[] HitBox;
    [Export] State HurtState;

    public override void _Ready()
    {
        foreach (var box in HitBox)
        {
            box.hit += hit;
        }
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
    public void hit()
    {
        //ChangeState(HurtState);
    }





}
