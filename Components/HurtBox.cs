using Godot;
using System;
using System.Collections.Generic;
[GlobalClass]
public partial class HurtBox : Area3D
{
    [Export] AnimationPlayer anim;
    [Export] float damage;
    [Signal] public delegate void hitEventHandler();
    [Signal] public delegate void destroyEventHandler(string _name);
    public List<Node> ignoreList = new();
    public List<Node> Inside = new();
    private Health bodyPartHealth;

    [Export] public bool hurt = false;

    public override void _Ready()
    {
        AreaEntered += areaEntered;
        AreaExited += areaExited;
    }
    public override void _PhysicsProcess(double delta)
    {
        if (hurt)
        {
            foreach(var i in Inside)
            {
                if(i is HitBox hb)
                {
                    if (!ignoreList.Contains(i))
                    {
                        if (ignoreList.Contains(hb))
                        {

                        }
                        else
                        {
                            GD.Print("Hurt This HitBox");
                            ignoreList.Add(hb);
                            hb.Hit(damage);
                            EmitSignal(SignalName.hit);
                            hurt = false;
                            if (anim != null)
                            {
                                if (anim.CurrentAnimation == "LeftBaked")
                                {
                                    hb.Side("HitLeft");
                                }
                                if (anim.CurrentAnimation == "RightBaked")
                                {
                                    hb.Side("HitRight");
                                }

                            }
                        }
                    }
                }
            }
        }
    }
    public void areaExited(Node3D area)
    {
        Inside.Remove(area);
    }
    public void areaEntered(Node3D area)
    {
        Inside.Add(area);
        if (area is HitBox hb && hurt)
        {
            if (ignoreList.Contains(hb))
            {

            }
            else
            {
                GD.Print("Hurt This HitBox");
                ignoreList.Add(hb);
                hb.Hit(damage);
                EmitSignal(SignalName.hit);
                hurt = false;
                if (anim != null)
                {
                    if (anim.CurrentAnimation == "LeftBaked")
                    {
                        hb.Side("HitLeft");
                    }
                    if(anim.CurrentAnimation == "RightBaked")
                    {
                        hb.Side("HitRight");
                    }

                }
            }
        }




    }
}