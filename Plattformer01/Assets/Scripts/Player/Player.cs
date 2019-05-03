using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{
    //Attributes
    [HideInInspector] public MeshRenderer Renderer;
    [HideInInspector] public PhysicsComponent physComp;
    [HideInInspector] public RayCasterCapsule rayCaster;

    [SerializeField] public bool kineticBatteryActive;
    [SerializeField] protected float mouseSensitivity;

    //Dash related attributes
    [SerializeField] protected Vector3 lastDash;

    [SerializeField] public Timer dashCooldownTimer;
    [SerializeField] public Timer dashDurationTimer;
    [SerializeField] public Timer doubleTapTimer;

    [SerializeField] protected float dashDistance;
    [HideInInspector] private bool doubleClick;

    // Methods
    protected override void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        rayCaster = GetComponent<RayCasterCapsule>();
        physComp = GetComponent<PhysicsComponent>();

        lastDash = Vector3.zero;

        base.Awake();
    }

    protected override void Update()
    {
        base.Update();

        dashCooldownTimer.SubtractTime();
        dashDurationTimer.SubtractTime();
        doubleTapTimer.SubtractTime();
    }

    public Vector3 ProcessVerticalInput()
    {
        RaycastHit hit = rayCaster.GetCollisionData(Vector3.down, 0.5f);
        Vector3 velocity = physComp.velocity;
        float skinWidth = physComp.skinWidth;
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(0, 0, verticalInput);
        input = Camera.main.transform.rotation * input.normalized;

        input = Vector3.ProjectOnPlane(input, hit.normal);
        input = input.normalized;

        return input;
    }

    public Vector3  ProcessHorizontalInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 input = new Vector3(horizontalInput, 0, 0);
        input = Camera.main.transform.rotation * input.normalized;

        input = input.normalized;

        return input;
    }

    public void AddPhysics()
    {
        physComp.SetDirection(ProcessVerticalInput());
        physComp.AddForces();
    }

    public void Dash()
    {
        //Calculating dash
        Vector3 dash = ProcessHorizontalInput() * dashDistance * Time.deltaTime;
        Vector3 dashRecoil = Vector3.zero;
        RaycastHit hit = rayCaster.GetCollisionData(dash , physComp.skinWidth);

        if (hit.collider != null){
            dashRecoil = Calculations2.CalculateNormalForce(dash, hit);
            physComp.velocity -= dashRecoil;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            if (doubleTapTimer.IsCountingDown())
            {
                doubleClick = true;
            }
            else
            {
                doubleTapTimer.SetTimer();
            }
        }

        if (dashDurationTimer.CheckLastFrame())
        {
            physComp.velocity -= lastDash;
        }

        //Executing dash
        if (dashCooldownTimer.IsReady() && doubleClick)
        {
            physComp.velocity += dash;
            dashCooldownTimer.SetTimer();
            dashDurationTimer.SetTimer();
            doubleTapTimer.RestartTimer();
            doubleClick = false;
            lastDash = dash;
        }
    }

    public bool GetKineticBatteryActive()
    {
        return kineticBatteryActive;
    }
}
