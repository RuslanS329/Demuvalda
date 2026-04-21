using Godot;
using System;

public partial class PickupCoin : Node3D
{
    [Export] int amount = 10;
    [Export] Area3D area;
    private bool isInside = false;
    private Inventory inventory;
    public override void _Ready()
    {
        area.BodyEntered += entered;
        area.BodyExited += exited;
    }
    public override void _Process(double delta)
    {
        if (isInside)
        {
            if (Input.IsActionJustPressed("e"))
            {
                inventory.addCoins(amount);
                QueueFree();
                //ADD coins
            }
        }
    }
    public void entered(Node3D body)
    {
        if (body is Player player)
        {
            isInside = true;
            inventory = player.inventory;
            GD.Print("Entered player");
        }
    }
    public void exited(Node3D body)
    {
        if (body is Player)
        {
            isInside = false;
            GD.Print("Player exited");
        }
    }
}
