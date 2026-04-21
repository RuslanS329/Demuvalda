using Godot;
using System;

public partial class MessageBoxSpawner : Node
{
    [Export] PackedScene box;
    [Export] Inventory inventory;
    [Export] float speed = 1.0f;
    [Export] float offset = 150f;
    public override void _Ready()
    {
        inventory.ItemAdded += message;
        inventory.coinsAdded += (int coins) => shootMessage("Coins: " + coins.ToString());

    }
    public void message(Item item)
    {
        shootMessage(item.name);
    }
    public void shootMessage(string name)
    {
        ItemPickedMessage msg = box.Instantiate() as ItemPickedMessage;
        msg.label.Text = name;
        Tween tween = GetTree().CreateTween();
        Vector2 pos = msg.Position;
        pos.Y = msg.Position.Y + offset;
        AddChild(msg);
        tween.SetEase(Tween.EaseType.In);
        tween.TweenProperty(msg, "position", msg.Position - new Vector2(0.0f,offset), speed);
        //tween.TweenProperty(msg, "position", msg.Position - new Vector2(-400f,offset), speed);
        tween.TweenProperty(msg, "modulate", new Color(0f, 0f, 0f, 0f),speed);
    }
}
