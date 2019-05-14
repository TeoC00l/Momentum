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
    [SerializeField] protected float strafeCoefficient;

        //Dash related attributes
    [SerializeField] protected Vector3 lastDash;

    [SerializeField] public Timer dashCooldownTimer;
    [SerializeField] public Timer dashDurationTimer;
    [SerializeField] public Timer doubleTapTimer;
    [SerializeField] public Timer kineticBatteryCooldownTimer;
    [SerializeField] private float kineticBatterySlidePower0Max1Min;


    [SerializeField] protected float dashDistance;
    [HideInInspector] protected bool isDashing;
    [HideInInspector] private bool doubleTap;
    [HideInInspector] public Vector3 oldVelocity;
    [HideInInspector] public int kineticTimer;
    [HideInInspector] public int divideValue;

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
        kineticBatteryCooldownTimer.SubtractTime();
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
        physComp.SetDirection(ProcessVerticalInput() + (ProcessHorizontalInput() *strafeCoefficient));
        physComp.AddForces();
    }

    public void Dash()
    {
        //Checking for collision to cancel dash
        if (isDashing == true)
        {
            RaycastHit hit = rayCaster.GetCollisionData(lastDash, physComp.skinWidth);

            if (hit.collider != null)
            {
                physComp.velocity -= lastDash;
                isDashing = false;
            }
        }

        //Checking for last frame of dash to cancel dash
        if (dashDurationTimer.CheckLastFrame() && isDashing == true)
        {
            physComp.velocity -= lastDash;
            isDashing = false;
        }

        //Checking for double tap
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            if (doubleTapTimer.IsCountingDown())
            {
                doubleTap = true;
            }
            else
            {
                doubleTapTimer.SetTimer();
            }
        }

        //Executing dash
        if (dashCooldownTimer.IsReady() && doubleTap)
        {
            //Calculating dash
            Vector3 dash = ProcessHorizontalInput() * dashDistance * Time.deltaTime;
            RaycastHit hit = rayCaster.GetCollisionData(dash, physComp.skinWidth);

            //Checking for collision to cancel dash
            if (hit.collider != null)
            {
                dash = Vector3.zero;
            }

            physComp.velocity += dash;
            dashCooldownTimer.SetTimer();
            dashDurationTimer.SetTimer();
            doubleTapTimer.RestartTimer();

            isDashing = true;
            doubleTap = false;

            lastDash = dash;
        }
    }
    public void DecreaseVelocity()
    {
        if (kineticTimer > 0)
        {
            Debug.Log("BEFORE" + "Player Class" + " this is the Velocity " + physComp.velocity + "this is the magnitude" + physComp.velocity.magnitude + "this is the Direction" +physComp.direction +"this is the timer"+ (kineticTimer - 1));
            physComp.direction = Vector3.zero;
            physComp.velocity -= oldVelocity / oldVelocity.magnitude * kineticBatterySlidePower0Max1Min;
            kineticTimer -= 1;
 
            if (Mathf.Sign(physComp.velocity.z) != Mathf.Sign(oldVelocity.z)|| Mathf.Sign(physComp.velocity.x) != Mathf.Sign(oldVelocity.x)|| kineticTimer == 0)
            {
                Debug.Log("Set Too zero" + kineticTimer);
                physComp.velocity = Vector3.zero;
            }
               
             Debug.Log("Player Class" + " this is the Velocity " + physComp.velocity + "this is the magnitude" + physComp.velocity.magnitude);
        }
    }


    public bool GetKineticBatteryActive()
    {
        return kineticBatteryActive;
    }
}
