using Godot;
using System;

public partial class PickUpSound : AudioStreamPlayer
{
    [Export] Inventory inventory;
    public override void _Ready()
    {
        inventory.ItemAdded += start;
        inventory.coinsAdded += (int i) => { Play(); };
    }
    public void start(Item item)
    {
        Play();
    }
}
