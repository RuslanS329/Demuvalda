using Godot;
using System;

public partial class Inventory : MarginContainer
{
    public bool isOpen = false;
    [Export] public Slot[] slots;
    [Export] Health playerHealth;
    [Signal] public delegate void ItemAddedEventHandler(Item item);
    [Signal] public delegate void ItemRemovedEventHandler(Item item);
    [Signal] public delegate void ItemUsedEventHandler(Item item);
    [Signal] public delegate void coinsChangedEventHandler(int coins);
    [Signal] public delegate void coinsAddedEventHandler(int coins);
    [Signal] public delegate void coinsRemovedEventHandler(int coins);
    public int coins = 0;
    public override void _Ready()
    {
        foreach(var slot in slots)
        {
            slot.ItemUsed += onItemUse;

            slot.ItemUsed += (Item i)=> { EmitSignal(SignalName.ItemUsed); };
            slot.ItemDropped += (Item i)=> { EmitSignal(SignalName.ItemUsed); };
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
    public bool haveCoins(int amount)
    {
        if(coins >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void removeCoins(int amount)
    {
        coins -= amount;
        EmitSignal(SignalName.coinsChanged, coins);
        EmitSignal(SignalName.coinsRemoved, amount);
    }
    public void addCoins(int amount)
    {
        coins += amount;
        EmitSignal(SignalName.coinsChanged, coins);
        EmitSignal(SignalName.coinsAdded, amount);
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
    public bool contains(Item item)
    {
        for(int i = 0;i < slots.Length; i++)
        {
            if (slots[i].item == item)
            {
                return true;
            }
        }
        return false;
    }
    public bool containsItems(Item[] items)
    {
        foreach(Item item in items)
        {
            if (!contains(item)) return false;
  

        }
        return true;
    }
    public bool take(Item item)
    {
        
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item)
            {
                slots[i].removeItem();
                return true;
            }
        }
        return false;
    }
}
