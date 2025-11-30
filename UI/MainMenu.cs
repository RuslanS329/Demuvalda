using Godot;
using System;

public partial class MainMenu : Control
{
    [Export] Button Play;
    [Export] Button Quit;
    [Export] string Game;
    public override void _Ready()
    {
        Play.Pressed += play;
        Quit.Pressed += quit;
    }
    public void play()
    {
        GetTree().ChangeSceneToFile(Game);
    }
    public void quit()
    {
        GetTree().Quit();
    }
    
}
