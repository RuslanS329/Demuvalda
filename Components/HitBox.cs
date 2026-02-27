using Godot;
using System;
[GlobalClass]
public partial class HitBox : Area3D
{
    [Signal] public delegate void hitEventHandler();
    [Signal] public delegate void destroyEventHandler(string _name);
    [Signal] public delegate void sideHitEventHandler(StringName _name);
    [Export] public string name;

    [Export] Health Health;

    //[Export] float damageToWholeBody = 0;

    //[Export] Node3D bodyPart;
    //[Export] bool destroyBodyPart = true;

    //[Export] PackedScene hitParticles;
    //[Export] PackedScene destroyParticles;

    private Health bodyPartHealth;
    public override void _Ready()
    {
        //bodyPartHealth = GetNode<Health>("Health");
        //bodyPartHealth.died += destroyed;
        //bodyPartHealth.hit += partHit;
    }
    public void Hit(float damage)
    {
        Health.getDamage(damage);

        //bodyPartHealth.health -= damage;

    }
    public void destroyed()
    {
        EmitSignal(SignalName.destroy, name);
        //bodyPart.QueueFree();
    }
    public void Side(string name)
    {
        EmitSignal(SignalName.sideHit, name);
    }


}