﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{
    //Attributes
    [HideInInspector] public MeshRenderer Renderer;
    [HideInInspector] public PhysicsComponent PhysComp;
    [HideInInspector] public Rigidbody rigid;

    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float strafeCoefficient;
    [SerializeField] private float kineticBatterySlidePower0Max1Min;

    private Checkpoint checkPoint;

    public RayCasterCapsule RayCaster;

    //Dash related attributes
    [SerializeField] private Vector3 lastDash;
    public Timer dashCooldownTimer;
    public Timer dashDurationTimer;
    [SerializeField] private float dashDistance;
    private bool isDashing;
    private bool neutralizeInput = false;

    //Kinetic battery related attributes
    [SerializeField] public int kineticTimer;
    [SerializeField] public int divideValue;
    public Timer kineticBatteryCooldownTimer;
    private bool stopKineticSlide = false;
    private bool currentlySliding = false;
    private Vector3 oldVelocity = Vector3.zero;

    //Rotation To surface (TEST)
    private Vector3 forwardRelativeToSurfaceNormal;
    private RaycastHit hit;
    private Vector3 surfaceNormal;


    // Methods
    protected override void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        RayCaster = GetComponent<RayCasterCapsule>();
        PhysComp = GetComponent<PhysicsComponent>();
        rigid = GetComponent<Rigidbody>();

        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        dashCooldownTimer.SubtractTime();
        dashDurationTimer.SubtractTime();
        kineticBatteryCooldownTimer.SubtractTime();
    }

    public Vector3 ProcessInput()
    {
        float skinWidth = PhysComp.GetSkinWidth();
        RaycastHit hit = RayCaster.GetCollisionData(Vector3.down, skinWidth * 2);
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector3 input = new Vector3(horizontalInput * strafeCoefficient, 0, verticalInput);
        input = Camera.main.transform.rotation * input.normalized;

        input = Vector3.ProjectOnPlane(input, hit.normal);
        input = input.normalized;

        return input;
    }

    public void AddPhysics()
    {
        if (neutralizeInput == false)
        {
            PhysComp.SetDirection(ProcessInput());
        }
        PhysComp.AddForces();
    }

    public void DecreaseVelocity()
    {
        if (kineticTimer > 0)
        {
            PhysComp.SetDirection(Vector3.zero);
            Vector3 NewVelocity = PhysComp.GetVelocity() - oldVelocity / oldVelocity.magnitude * kineticBatterySlidePower0Max1Min;
            PhysComp.SetVelocity(NewVelocity);
            kineticTimer -= 1;

            if (PhysComp.GetVelocity().magnitude < 0.2f && GetStopKineticSlide() == false || kineticTimer == -1000 && GetStopKineticSlide() == false)
            {
                Debug.Log("SET ZERO");
                SetStopKineticSlide(true);
                SetCurrentlySliding(true);
                kineticTimer = 0;
             //   PhysComp.SetVelocity(Vector3.zero);
                CancelInvoke("DecreaseVelocity");

            }
        }
    }

    //GETTERS AND SETTERS
    public Vector3 GetOldVelocity()
    {
        return oldVelocity;
    }
    public void SetOldVelocity(Vector3 set)
    {
        this.oldVelocity = set;
    }
    public bool GetKineticActive()
    { 
        if (GetCurrentStateType() == typeof(KineticBatteryState) || GetCurrentStateType() == typeof(KineticBatteryAirbourneState))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool GetMomentumActive()
    {
        if (GetCurrentStateType() == typeof(MomentumState) || GetCurrentStateType() == typeof(MomentumAirbourneState))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool GetPrecisionActive()
    {
        if (GetCurrentStateType() == typeof(PrecisionState) || GetCurrentStateType() == typeof(PrecisionAirbourneState))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SetStrafeCoefficient(float strafeCoefficient)
    {
        this.strafeCoefficient = strafeCoefficient;
    }
    public void SetNeutralizeInput(bool set)
    {
        this.neutralizeInput = set;
    }
    public bool GetStopKineticSlide()
    {
        return stopKineticSlide;
    }
    public void SetStopKineticSlide(bool set)
    {
        this.stopKineticSlide = set;
    }
    public bool GetCurrentlySliding()
    {
        return currentlySliding;
    }
    public void SetCurrentlySliding(bool set)
    {
        this.currentlySliding = set;
    }
}
