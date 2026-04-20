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



    public override void _Ready()
    {
        useButton.Pressed += use;
        dropButton.Pressed += drop;
        update(item);
        
        MouseEntered += () =>
        {
            if (item == null) return;
            var tween = GetTree().CreateTween().SetPauseMode(TweenPauseMode.Process);
            tween.TweenProperty(rect, "scale", new Vector2(1.3f, 1.3f), 0.1f);
            tween.TweenProperty(rect, "rotation", 0.04f, 0.1f);
            tween.TweenProperty(rect, "rotation", -0.04f, 0.1f);
            itemAudio.Play();
            //rect.Scale = new(1.3f,1.3f);
        };
        MouseExited += () =>
        {
            if (item == null) return;
            var tween = GetTree().CreateTween().SetPauseMode(TweenPauseMode.Process);
            tween.TweenProperty(rect, "scale", new Vector2(1.0f, 1.0f), 0.1f);
            tween.TweenProperty(rect, "rotation", 0f, 0.1f);
            
            //rect.Scale = new(1.0f, 1.0f);
        };

    }


    public void use()
    {
        EmitSignal(SignalName.ItemUsed, item);
        if(item.usedAudio != null) itemUsedAudio.Play();
        update(null);


    }
    public void drop()
    {
        EmitSignal(SignalName.ItemDropped, item);
        update(null);

    }
    public void update(Item i)
    {
        if (i != null)
        {
            item = i;
            rect.Texture = i.image;
            itemName.Text = i.name;
            if(item.audio!=null)
            itemAudio.Stream = i.audio;
            if (item.usedAudio != null)
                itemUsedAudio.Stream = i.usedAudio;
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
            item = null;
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
