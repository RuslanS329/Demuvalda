using Godot;
using System;

public partial class MainMenuSFX : Node
{
    [Export] AudioStreamPlayer hsfx;
    [Export] AudioStreamPlayer psfx;
    [Export] Button[] buttons;
    public override void _Ready()
    {
        foreach(Button b in buttons){
            b.Pressed += ()=> { psfx.Play(); } ;
            b.MouseEntered += () => { hsfx.Play(); };
        }
    }
 

}
