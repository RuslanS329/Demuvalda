using Godot;
using System;

public partial class OnPlayerLose : Node
{
    [Export] Health hp;
    [Export] String game;
    public override void _Ready()
    {
        hp.died += lose;
    }
    public void lose()
    {
        GetTree().ChangeSceneToFile(game);
    }
}
