using Godot;
using System;
[GlobalClass]
public partial class HealthAction : ItemAction
{
    [Export] public float heal = 100;
    [Export] public float overTime = 1.0f;
}
