using Godot;
using System;
using static Godot.TextServer;

public partial class Player : CharacterBody3D
{
    [Export] Node3D cam;
    [Export] HeadBob headBob;


    [Export] float mouse_sensitivity = 0.08f;

    [Export] float gravity = -20f;
    [Export] float JumpVelocity = 6f;
    [Export] float holdJumpMultiplier = 0.5f;
    [Export] Curve holdJumpCurve;
    [Export] float holdJumpDuraton = 0.5f;
    private float holdJumpTimer = 0.0f;
    private bool holdJumpHold = false;
    [Export] public bool coyoteTime = true;
    [Export] public float coyoteTimeDuration = 0.5f;
    private bool jumped = false;

    [Export] float Speed = 6.0f;
    [Export] float WalkSpeed = 6.0f;
    [Export] float RunSpeed = 10.0f;
    public float speedMultiplier = 1.0f;
    float acceleration = 0.2f;



    private float timeInAir = 0f;
    private float minTimeToLandAnimation = 0.4f;
    private bool isInAir = false;
    private float positionOfJump = 0.0f;

    public Vector3 velocity;
    public Vector3 boost;


    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }
    public override void _PhysicsProcess(double delta)
    {

        velocity = Velocity;

        // Gravity and timeinAir
        if (!IsOnFloor())
        {
            velocity = applyGravity(velocity, (float)delta);

            timeInAir += (float)delta;
        }

        calculateLandAnimation();

        //Run Or Walk Speed
        CalculateSpeed();

        //CalculateJump((float)delta);

        //Jump.
        velocity = CalculateJump(velocity, (float)delta);
        //Direction Movement

        Vector2 inputDir = getInputDirection();

        Vector3 direction = getDirection(inputDir);

        velocity = CalculateVelocityXZ(velocity, direction);



        //Bob and Tilt
        CalculateBob(inputDir);


        boost = boost.MoveToward(Vector3.Zero, (float)delta * 25f);

        Velocity = velocity;
        MoveAndSlide();


    }
    public void CalculateSpeed()
    {
        float targetSpeed = Input.IsActionPressed("shift") ? RunSpeed : WalkSpeed;

        Speed = Mathf.Lerp(Speed, targetSpeed, acceleration);
    }
    public Vector3 CalculateVelocityXZ(Vector3 velocity, Vector3 direction)
    {
        //INPUT
        if (direction != Vector3.Zero)
        {
            //Lerp Velocity To Speed
            velocity = lerpVelocityXZ(velocity, direction + boost, Speed);
        }
        //NO INPUT
        else
        {
            //GROUND
            if (IsOnFloor())
            {
                //Lerp Velocity To 0
                velocity = lerpVelocityXZ(velocity, direction + boost, 0);

            }
        }
        return velocity;
    }
    public Vector3 CalculateJump(Vector3 velocity, float delta)
    {
        if (IsOnFloor())
        {
            holdJumpTimer = 0.0f;
            jumped = false;
        }



        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            velocity.Y = JumpVelocity;
            holdJumpHold = true;
            jumped = true;

        }
        else if (Input.IsActionJustPressed("jump") && timeInAir < coyoteTimeDuration && !jumped)
        {
            velocity.Y = JumpVelocity;
            holdJumpHold = true;
            jumped = true;
        }
        if (!IsOnFloor() && !Input.IsActionPressed("jump"))
        {
            holdJumpHold = false;
        }
        if (Input.IsActionPressed("jump") && !IsOnFloor() && holdJumpHold && holdJumpTimer < holdJumpDuraton)
        {
            velocity.Y += holdJumpCurve.Sample(holdJumpDuraton);
            holdJumpTimer += delta;
        }




        return velocity;
    }
    public void CalculateBob(Vector2 input)
    {
        Vector2 speed = new Vector2(Velocity.X, Velocity.Z);
        headBob.bobPercent = speed.Length() / RunSpeed;
        if (!IsOnFloor())
        {
            headBob.bobPercent = 0f;
        }
        headBob.input = input;
    }
    public void calculateLandAnimation()
    {
        if (!IsOnFloor() && !isInAir)
        {
            isInAir = true;
            positionOfJump = GlobalPosition.Y;

        }
        if (IsOnFloor() && isInAir)
        {
            isInAir = false;
            if (timeInAir > minTimeToLandAnimation && GlobalPosition.Y < positionOfJump)
            {
            }
            timeInAir = 0f;

        }
    }
    public Vector3 applyGravity(Vector3 velocity, float delta)
    {
        velocity.Y += gravity * delta;
        return velocity;
    }
    public Vector2 getInputDirection()
    {
        return Input.GetVector("left", "right", "forward", "back");
    }
    public Vector3 getDirection(Vector2 inputDir)
    {
        return (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
    }
    public Vector3 lerpVelocityXZ(Vector3 velocity, Vector3 direction, float speed)
    {
        velocity.X = Mathf.Lerp(Velocity.X, direction.X * speed * speedMultiplier, acceleration);
        velocity.Z = Mathf.Lerp(Velocity.Z, direction.Z * speed * speedMultiplier, acceleration);

        return velocity;
    }
    public override void _Input(InputEvent @event)
    {
        if (Input.MouseMode == Input.MouseModeEnum.Captured)
            if (@event is InputEventMouseMotion mot)
            {
                this.RotateY(Mathf.DegToRad(-mot.Relative.X * mouse_sensitivity));
               // cam.RotateX(Mathf.DegToRad(-mot.Relative.Y * mouse_sensitivity));
                // cam.RotationDegrees = new Vector3(Mathf.Clamp(cam.RotationDegrees.X, -80f, 80f), cam.RotationDegrees.Y, cam.RotationDegrees.Z);
                float rot_x = cam.Rotation.X + (Mathf.DegToRad(-mot.Relative.Y * mouse_sensitivity));
                rot_x = Mathf.Clamp(rot_x, Mathf.DegToRad(-80), Mathf.DegToRad(80));
                cam.Rotation = new(rot_x, 0, 0);

            }
    }




}