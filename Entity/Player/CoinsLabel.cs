using Godot;
using System;

public partial class CoinsLabel : Label
{
    [Export] Inventory inventory;
    public override void _Ready()
    {
        Text = "Coins: " + inventory.coins.ToString();
        inventory.coinsChanged += update;
    }
    public void update(int amount)
    {
        Text = "Coins: " + amount;
    }
}
