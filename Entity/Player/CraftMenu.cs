using Godot;
using System;

public partial class CraftMenu : PanelContainer
{
    [Export] Inventory inventory;
    [Export] Item ointment;
    [Export] Item bandage;
    [Export] Item patch;
    [Export] Button sBandageButton;
    [Export] Button sPatchButton;
    [Export] Item superBan;
    [Export] Item superPatch;



    public override void _Ready()
    {

        inventory.ItemAdded += update;
        inventory.ItemRemoved += update;
        sBandageButton.Pressed += craftBandageOint;
        sPatchButton.Pressed += craftPatchOint;
        update(null);
    }
    public void craft()
    {

    }
    public void craftBandageOint()
    {
        inventory.take(ointment);
        inventory.take(bandage);
        inventory.addItem(superBan);
        update(null);
    }
    public void craftPatchOint()
    {
        inventory.take(ointment);
        inventory.take(patch);
        inventory.addItem(superPatch);
        update(null);
    }

    public void update(Item item)
    {
        sBandageButton.Visible = inventory.containsItems(new Item[] { bandage, ointment });
        sPatchButton.Visible = inventory.containsItems(new Item[] { patch, ointment });
        if (sBandageButton.Visible || sPatchButton.Visible) 
        {
            show();
        }
        else
        {
            hide();
        }
    }
    public void hide()
    {
        Visible = false;
        //var tween = GetTree().CreateTween();
        //tween.SetEase(Tween.EaseType.In);
        //tween.TweenProperty(this, "position.x", Position.X + Size.X + 100f, 0.5f);
    }
    public void show()
    {
        Visible = true;
        //var tween = GetTree().CreateTween();
        //tween.SetEase(Tween.EaseType.In);
        //tween.TweenProperty(this, "position", Position.X - Size.X - 100f, 0.5f);
    }
}
