using Godot;
using System;

public partial class Inventory : MarginContainer
{
    public bool isOpen = false;
    [Export] Slot[] slots;
    [Export] Health playerHealth;
    public override void _Ready()
    {
        foreach(var slot in slots)
        {
            slot.ItemUsed += onItemUse;
        }
    }

    public override void _PhysicsProcess(double delta)
    {

        if (Input.IsActionJustPressed("inventory"))
        {
            if (isOpen) close();
            else open();

            
        }
    }
    public void close()
    {
        Visible = false;
        Input.MouseMode = Input.MouseModeEnum.Captured;
        isOpen = false;
        GetTree().Paused = false;
    }
    public void open()
    {
        Visible = true;
        Input.MouseMode = Input.MouseModeEnum.Visible;
        isOpen = true;
        GetTree().Paused = true;
    }
    public void onItemUse(Item item)
    {
        if (item.Actions != null)
        {
            foreach (ItemAction action in item.Actions)
            {
                if(action is HealthAction ha)
                {
                    playerHealth.getHeal(ha.heal);
                }
            }
        }
    }
}
