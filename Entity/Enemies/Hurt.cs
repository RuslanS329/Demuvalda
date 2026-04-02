using Godot;
using System;

public partial class Hurt : State
{
    [Export] State bite;
    [Export] AnimationPlayer anim;
    public override void Enter()
    {
        
        GetTree().CreateTimer(0.4).Timeout+=timeout;
    }
    public void timeout()
    {
        st.ChangeState(bite.StateName);
    }
    
}
