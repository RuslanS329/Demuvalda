using Godot;
using System;
using static Godot.Tween;

public partial class Slot : Control
{
    [Export] public Item item;
    [Export] TextureRect rect;
    [Export] Label itemName;
    [Export] Button dropButton;
    [Export] Button useButton;
    [Export] AudioStreamPlayer itemAudio;
    [Export] AudioStreamPlayer itemUsedAudio;

    [Signal] public delegate void ItemUsedEventHandler(Item item);
    [Signal] public delegate void ItemDroppedEventHandler(Item item);
    public bool occupied = false;



    public override void _Ready()
    {
        
        if (item != null)
        {
            addItem(item);
        }
        else
        {
            removeItem();
        }
        useButton.Pressed += use;
        dropButton.Pressed += drop;

        MouseEntered += () =>
        {
            if (occupied)
            {
                var tween = GetTree().CreateTween().SetPauseMode(TweenPauseMode.Process);
                tween.TweenProperty(rect, "scale", new Vector2(1.3f, 1.3f), 0.1f);
                tween.TweenProperty(rect, "rotation", 0.04f, 0.1f);
                tween.TweenProperty(rect, "rotation", -0.04f, 0.1f);
                if (itemAudio.Stream != null)
                    itemAudio.Play();

            }
        };
        MouseExited += () =>
        {

            if (occupied)
            {
                var tween = GetTree().CreateTween().SetPauseMode(TweenPauseMode.Process);
                tween.TweenProperty(rect, "scale", new Vector2(1.0f, 1.0f), 0.1f);
                tween.TweenProperty(rect, "rotation", 0f, 0.1f);

            }

        };

    }


    public void use()
    {
        EmitSignal(SignalName.ItemUsed, item);
        if (item.usedAudio != null) itemUsedAudio.Play();
        removeItem();


    }
    public void drop()
    {
        EmitSignal(SignalName.ItemDropped, item);
        removeItem();

    }
    public void addItem(Item i)
    {
        GD.Print("Added item " + i.name);
        occupied = true;
        item = i;
        rect.Texture = i.image;
        itemName.Text = i.name;

        if (item.audio != null) itemAudio.Stream = i.audio;

        if (item.usedAudio != null) itemUsedAudio.Stream = i.usedAudio;

        buttons(true);

        if (i is KeyItem)
        {
            buttons(false);
        }
    }
    public void removeItem()
    {
        occupied = false;
        item = null;
        GD.Print("Remove item");
        rect.Texture = null;
        itemName.Text = null;
        buttons(false);

    }
    public void update(Item i)
    {
        item = i;
        if (i != null)
        {

            rect.Texture = i.image;
            itemName.Text = i.name;

            if (item.audio != null) itemAudio.Stream = i.audio;

            if (item.usedAudio != null) itemUsedAudio.Stream = i.usedAudio;

            buttons(true);

            if ((i is KeyItem))
            {
                buttons(false);
            }

        }
        else
        {
            itemAudio = null;
            itemUsedAudio = null;
            rect.Texture = null;
            itemName.Text = "";
            buttons(false);
        }

    }
    public void buttons(bool hide)
    {
        useButton.Visible = hide;
        dropButton.Visible = hide;
    }
}
