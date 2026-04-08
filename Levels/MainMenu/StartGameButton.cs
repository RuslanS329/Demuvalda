using Godot;
using System;


public partial class StartGameButton : Button
{
    [Export] String game;
    public override void _Ready()
    {
        Pressed += () => { GetTree().ChangeSceneToFile(game); };
        
    }
}
