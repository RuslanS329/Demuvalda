using Godot;
using System;
[GlobalClass]
public partial class Item : Resource
{
    [Export] public Texture2D image;
    [Export] public string name;
    [Export] public Godot.Collections.Array<ItemAction> Actions;
    [Export] public AudioStream audio;
    [Export] public AudioStream usedAudio;
}
