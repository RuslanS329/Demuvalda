using Godot;
using System;

public partial class DvuNogSM : StateMachine
{
 
    [Export] Label debugl;
    //[Export] HitBox[] HitBox;
    [Export] State HurtState;

    public override void _Ready()
    {
        //foreach (var box in HitBox)
        //{
        //    box.hit += hit;
        //}
        
        init();
    }
    public void hit()
    {
        //ChangeState(HurtState.StateName);
    }





}
