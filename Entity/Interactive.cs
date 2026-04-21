using Godot;
using System;
[GlobalClass]
public partial class Interactive : Area3D
{


    [Signal] public delegate void triedInteractEventHandler();
    [Signal] public delegate void activatedEventHandler();

    [Export] public Item item;
    [Export] public bool takeItem = true;
    [Export] bool oneTime = true;
    [Export] AudioStream audio;
    [Export] AudioStreamPlayer audioPlayer;


    Inventory inventory;

    private bool isInside = false;
    private bool used = false;
    public override void _Ready()
    {
        BodyEntered += entered;
        BodyExited += exited;
    }
    public override void _PhysicsProcess(double delta)
    {
        if (used && oneTime) return;

        if (isInside)
        {
            if (Input.IsActionJustPressed("e"))
            {
                if (item == null)
                {
                    EmitSignal(SignalName.activated);
                    used = true;
                }
                else
                {
                    
                    if (inventory.contains(item)){
                        used = true;
                        EmitSignal(SignalName.activated);
                        if(audio != null)
                        {
                            audioPlayer.Stream = audio;
                            audioPlayer.Play();
                        }
                        if (takeItem)
                        {
                            inventory.take(item);
                            
                        }
                    }
                    else
                    {
                        EmitSignal(SignalName.triedInteract);
                    }
                }
            }
        }
    }
    public void entered(Node3D body)
    {
        if(body is Player p)
        {
            isInside = true;
            inventory = p.inventory;
        }
    }
    public void exited(Node3D body)
    {
        if (body is Player p)
        {
            isInside = false;
        }
    }

}
