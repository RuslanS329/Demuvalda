using Godot;
using System;
[GlobalClass]
public partial class DeleteParentTimer : Timer
{
    public override void _Ready()
    {
        Timeout += timeout;
    }
    public void timeout()
    {
        var p = GetParent();
        if(p != null)
        {
            p.QueueFree();
        }
    }
}
