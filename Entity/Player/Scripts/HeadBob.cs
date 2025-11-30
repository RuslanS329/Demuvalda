using Godot;
using System;

public partial class HeadBob : Node3D
{
    [Export] float amplitude = .25f;
    [Export] float freq = .5f;
    [Export] float tilt_y_speed = 0.1f;
    [Export] float tilt_y_amplitude = 1f;
    [Export] float tilt_x_speed = 0.1f;
    [Export] float tilt_x_amplitude = 1f;

    [Export] float run_tilt_amplitude = 1f;
    [Export] float run_tilt_speed = 0.1f;

    public bool bob = false;
    public bool tilt = true;
    public Vector2 input = Vector2.Zero;
    public float bobPercent = 0.0f;
    private float temp;
    private float tilt_x = 0f;
    private float tilt_y = 0f;
    private float heightOffset = 0f;
    private Vector3 rotation;

    
    public override void _Ready()
    {
        heightOffset = Position.Y;
    }

    public override void _PhysicsProcess(double delta)
    {
        bbob(delta);
        ttilt();

    }
    void bbob(double delta)
    {
        temp += (float)delta;
        Position = new Vector3(0, Mathf.Sin(temp * freq) * amplitude * bobPercent + heightOffset, 0);
    }
    void ttilt()
    {
        rotation = RotationDegrees;
        //TILT X
        if (input.X != 0)
        {
            tilt_x = Mathf.Lerp(tilt_x, -tilt_x_amplitude * input.X, tilt_x_speed);
        }
        else
        {
            tilt_x = Mathf.Lerp(tilt_x, 0, tilt_x_speed);
        }
        //TILT Y
        if (input.Y != 0)
        {
            tilt_y = Mathf.Lerp(tilt_y, tilt_y_amplitude * input.Y, tilt_y_speed);
        }
        else
        {
            tilt_y = Mathf.Lerp(tilt_y, 0, tilt_y_speed);
        }

        rotation.Z = tilt_x + (Mathf.Sin(temp * run_tilt_speed) * run_tilt_amplitude - 0.5f) * bobPercent;
        rotation.X = tilt_y;
        RotationDegrees = rotation;
    }
}