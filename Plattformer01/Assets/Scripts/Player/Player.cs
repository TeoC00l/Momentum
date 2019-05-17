using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{
    //Attributes
    [HideInInspector] public MeshRenderer Renderer;
    [HideInInspector] public PhysicsComponent PhysComp;

    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float strafeCoefficient;
    [SerializeField] private float kineticBatterySlidePower0Max1Min;

    private RayCasterCapsule rayCaster;

    //Dash related attributes
    [SerializeField] private Vector3 lastDash;
    [SerializeField] private Timer dashCooldownTimer;
    [SerializeField] private Timer dashDurationTimer;
    [SerializeField] private Timer doubleTapTimer;
    public Timer kineticBatteryCooldownTimer;

    [SerializeField] private float dashDistance;
    private bool isDashing;
    private bool doubleTap;
    private Vector3 oldVelocity = Vector3.zero;

    //Kinetic battery related attributes
    private int kineticCounter;
    private int kineticSlideDivideValue;
    private bool kineticActive = false;

    // Methods
    protected override void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        rayCaster = GetComponent<RayCasterCapsule>();
        PhysComp = GetComponent<PhysicsComponent>();

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
        Vector3 velocity = PhysComp.GetVelocity();
        float skinWidth = PhysComp.GetSkinWidth();
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
        PhysComp.SetDirection(ProcessVerticalInput() + (ProcessHorizontalInput() *strafeCoefficient));
        PhysComp.AddForces();
    }

    public void DecreaseVelocity()
    {
        if (kineticCounter > 0)
        {
        //    Debug.Log("BEFORE" + "Player Class" + " this is the Velocity " + PhysComp.GetVelocity() + "this is the magnitude" + PhysComp.GetVelocity().magnitude + "this is the Direction" +PhysComp.GetDirection() +"this is the timer"+ (kineticCounter - 1));
            PhysComp.SetDirection(Vector3.zero);
            Vector3 NewVelocity = PhysComp.GetVelocity() - oldVelocity / oldVelocity.magnitude * kineticBatterySlidePower0Max1Min;
            PhysComp.SetVelocity(NewVelocity);
            kineticCounter -= 1;
 
            if (Mathf.Sign(PhysComp.GetVelocity().z) != Mathf.Sign(oldVelocity.z)|| Mathf.Sign(PhysComp.GetVelocity().x) != Mathf.Sign(oldVelocity.x)|| kineticCounter == 0)
            {
                kineticCounter = 0;
                //          Debug.Log("Set Too zero" + kineticCounter);
                PhysComp.SetVelocity(Vector3.zero);
            }
               
       //      Debug.Log("Player Class" + " this is the Velocity " + PhysComp.GetVelocity() + "this is the magnitude" + PhysComp.GetVelocity().magnitude);
        }
    }

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

        return kineticActive;
    }
    public void SetKineticActive(bool set)
    {

        kineticActive = set;
    }

    public void SetKineticSlideDivideValue(int kineticSlideDivideValue)
    {
        this.kineticSlideDivideValue = kineticSlideDivideValue;
    }

    public int GetKineticCounter()
    {
        return kineticCounter;
    }

    public void SetKineticCounter(int kineticCounter)
    {
        this.kineticCounter = kineticCounter;
    }













    //public void Dash()
    //{
    //    //Checking for collision to cancel dash
    //    if (isDashing == true)
    //    {
    //        RaycastHit hit = rayCaster.GetCollisionData(lastDash, PhysComp.GetSkinWidth());

    //        if (hit.collider != null)
    //        {
    //            PhysComp.SetVelocity -= lastDash;
    //            isDashing = false;
    //        }
    //    }

    //    //Checking for last frame of dash to cancel dash
    //    if (dashDurationTimer.CheckLastFrame() && isDashing == true)
    //    {
    //        PhysComp.velocity -= lastDash;
    //        isDashing = false;
    //    }

    //    //Checking for double tap
    //    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
    //    {
    //        if (doubleTapTimer.IsCountingDown())
    //        {
    //            doubleTap = true;
    //        }
    //        else
    //        {
    //            doubleTapTimer.SetTimer();
    //        }
    //    }

    //    //Executing dash
    //    if (dashCooldownTimer.IsReady() && doubleTap)
    //    {
    //        //Calculating dash
    //        Vector3 dash = ProcessHorizontalInput() * dashDistance * Time.deltaTime;
    //        RaycastHit hit = rayCaster.GetCollisionData(dash, PhysComp.skinWidth);

    //        //Checking for collision to cancel dash
    //        if (hit.collider != null)
    //        {
    //            dash = Vector3.zero;
    //        }

    //        PhysComp.velocity += dash;
    //        dashCooldownTimer.SetTimer();
    //        dashDurationTimer.SetTimer();
    //        doubleTapTimer.RestartTimer();

    //        isDashing = true;
    //        doubleTap = false;

    //        lastDash = dash;
    //    }
    //}

}
