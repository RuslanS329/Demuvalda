using Godot;
using System;

public partial class Inventory : MarginContainer
{
    public bool isOpen = false;
    [Export] public Slot[] slots;
    [Export] Health playerHealth;
    [Signal] public delegate void ItemAddedEventHandler(Item item);
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
    public bool addItem(Item item)
    {
        GD.Print("AddItem");
        for(int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].occupied)
            {
                slots[i].addItem(item);
                EmitSignal(SignalName.ItemAdded, item);
                return true;
            }
        }
        return false;
        
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
