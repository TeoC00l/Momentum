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

    public RayCasterCapsule RayCaster;

    //Dash related attributes
    [SerializeField] private Vector3 lastDash;
    public Timer dashCooldownTimer;
    [SerializeField] private Timer dashDurationTimer;
    [SerializeField] private Timer doubleTapTimer;
    public Timer kineticBatteryCooldownTimer;

    [SerializeField] private float dashDistance;
    private bool isDashing;
    private bool doubleTap;
    private Vector3 oldVelocity = Vector3.zero;

    //Kinetic battery related attributes
    [SerializeField] public int kineticTimer;
    [SerializeField] public int divideValue;

    // Methods
    protected override void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        RayCaster = GetComponent<RayCasterCapsule>();
        PhysComp = GetComponent<PhysicsComponent>();
        rigid = GetComponent<Rigidbody>();

        lastDash = Vector3.zero;

        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        dashCooldownTimer.SubtractTime();
        dashDurationTimer.SubtractTime();
        doubleTapTimer.SubtractTime();
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
        PhysComp.SetDirection(ProcessInput());
        PhysComp.AddForces();
    }

    public void DecreaseVelocity()
    {
        if (kineticTimer > 0)
        {
            //    Debug.Log("BEFORE" + "Player Class" + " this is the Velocity " + PhysComp.GetVelocity() + "this is the magnitude" + PhysComp.GetVelocity().magnitude + "this is the Direction" +PhysComp.GetDirection() +"this is the timer"+ (kineticTimer - 1));
            PhysComp.SetDirection(Vector3.zero);
            Vector3 NewVelocity = PhysComp.GetVelocity() - oldVelocity / oldVelocity.magnitude * kineticBatterySlidePower0Max1Min;
            PhysComp.SetVelocity(NewVelocity);
            kineticTimer -= 1;

            if (Mathf.Sign(PhysComp.GetVelocity().z) != Mathf.Sign(oldVelocity.z) || Mathf.Sign(PhysComp.GetVelocity().x) != Mathf.Sign(oldVelocity.x) || kineticTimer == 0)
            {
                kineticTimer = 0;
                //          Debug.Log("Set Too zero" + kineticTimer);
                PhysComp.SetVelocity(Vector3.zero);
            }

            //      Debug.Log("Player Class" + " this is the Velocity " + PhysComp.GetVelocity() + "this is the magnitude" + PhysComp.GetVelocity().magnitude);
        }
    }

    //public void dash()
    //{
    //    //checking for collision to cancel dash
    //    if (isDashing == true)
    //    {
    //        RaycastHit hit = rayCaster.GetCollisionData(lastDash, PhysComp.GetSkinWidth());

    //        if (hit.collider != null)
    //        {
    //            PhysComp.SubtractVelocity(lastDash);
    //            isDashing = false;
    //        }
    //    }

    //    //checking for last frame of dash to cancel dash
    //    if (dashDurationTimer.CheckLastFrame() && isDashing == true)
    //    {
    //        PhysComp.SubtractVelocity(lastDash);
    //        isDashing = false;
    //    }

    //    //executing dash
    //    if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
    //    {
    //        Vector3 dash = Vector3.zero;

    //        //calculating dash
    //        if (Input.GetKeyDown(KeyCode.Q))
    //        {
    //            dash = Vector3.left * dashDistance * Time.deltaTime;
    //        }

    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            dash = Vector3.right * dashDistance * Time.deltaTime;
    //        }

    //        RaycastHit hit = rayCaster.GetCollisionData(dash, PhysComp.GetSkinWidth());

    //        //checking for collision to cancel dash
    //        if (hit.collider != null)
    //        {
    //            dash = Vector3.zero;
    //        }

    //        PhysComp.AddVelocity(dash);
    //        dashCooldownTimer.SetTimer();
    //        dashDurationTimer.SetTimer();

    //        isDashing = true;

    //        lastDash = dash;
    //    }
    //}

    //GETTERS AND SETTERS
    public Vector3 GetOldVelocity()
    {
        return oldVelocity;
    }

    public void SetOldVelocity(Vector3 set)
    {
        oldVelocity = set;
    }

    public bool GetKineticActive()
    { 
        if (GetCurrentStateType() == typeof(KineticBatteryState) || GetCurrentStateType() == typeof(KineticBatteryAirbourneState))
        {
            Debug.Log("KineticActive");

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

   
}
