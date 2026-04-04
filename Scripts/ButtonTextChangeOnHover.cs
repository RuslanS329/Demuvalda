using Godot;
using System;
using System.ComponentModel;
[GlobalClass]
public partial class ButtonTextChangeOnHover : Node
{
    Button button;
    [Export] string hoverText = "";
    private string defaultText = "";
    public override void _Ready()
    {
        button = GetParent<Button>();
        defaultText = button.Text;
        button.MouseEntered += () => { button.Text = hoverText; };
        button.MouseExited += () => { button.Text = defaultText; };
    }
    
    
}
