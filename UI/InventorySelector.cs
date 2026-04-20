using Godot;
using System;

public partial class InventorySelector : Panel
{
    [Export] Button use;
    [Export] Button delete;
    public bool mouse = false;
    public override void _Ready()
    {
        base._Ready();
        MouseEntered += () => mouse = true;
        MouseExited += () => mouse = false;
    }
}
