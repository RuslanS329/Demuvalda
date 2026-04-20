using Godot;
using System;

public partial class Attack : State
{
    [Export] AnimationPlayer animationPlayer;
    [Export] string attackAnimation;
    [Export] Monster monster;
    [Export] float speed = 8.0f;
    public override void Enter()
    {
        Vector3 direction = (-monster.nextPath + monster.playerPosition).Normalized();
        monster.move(direction * speed);
        animationPlayer.Play(attackAnimation);

    }
    public override void _Ready()
    {
        base._Ready();
        animationPlayer.AnimationFinished += animationFinished;

    }

    public void animationFinished(StringName name)
    {
        if(name == attackAnimation)
        st.ChangeState("BackUp");
    }
}
