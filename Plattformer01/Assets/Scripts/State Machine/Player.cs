using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{
    //Attributes
    [HideInInspector] public MeshRenderer Renderer;
    [HideInInspector] public PhysicsComponent physComp;
    [HideInInspector] public RayCasterCapsule rayCaster;

    [SerializeField] protected float mouseSensitivity = 3.0f;

    //Dash related attributes
    [SerializeField] protected Vector3 lastDash;

    [SerializeField] protected float dashDuration;
    [SerializeField] protected float dashCooldown;
    [SerializeField] protected float dashDurationTimer;
    [SerializeField] protected float dashCooldownTimer;
    [SerializeField] protected float dashDistance;

    [SerializeField] protected float doubleClickTimer;
    [HideInInspector] protected float doubleClickTime;
    [HideInInspector] protected bool doubleClick;

    // Methods
    protected override void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        rayCaster = GetComponent<RayCasterCapsule>();
        physComp = GetComponent<PhysicsComponent>();

        dashCooldown = 0.5f;
        dashDuration = 0.1f;
        dashDistance = 70f;
        doubleClickTime = 0.5f;
        doubleClickTimer = 0f;

        mouseSensitivity = 3.0f;

        lastDash = Vector3.zero;

        base.Awake();
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
        RaycastHit hit = rayCaster.GetCollisionData(dash * 6, 0);

        if (hit.collider != null){
            dash = Vector3.zero;
        }

        //Adjusting timers

        if(doubleClickTimer > 0 )
        {
            doubleClickTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            if (doubleClickTimer > 0)
            {
                doubleClick = true;
            }
            else
            {
                doubleClickTimer = doubleClickTime;
                doubleClick = false;
            }
        }

        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (dashDurationTimer > 0)
        {
            dashDurationTimer -= Time.deltaTime;

            //Resetting velocity at last frame of dash
            if (dashDurationTimer <= 0)
            {
                physComp.velocity -= lastDash;
            }
        }

        //Executing dash
        if (dashCooldownTimer <= 0 && doubleClick)
        {
            Debug.Log("Scoop");
            physComp.velocity += dash;
            dashCooldownTimer += dashCooldown;
            dashDurationTimer += dashDuration;
            doubleClickTimer = 0;
            doubleClick = false;
            lastDash = dash;
        }
    }
}
