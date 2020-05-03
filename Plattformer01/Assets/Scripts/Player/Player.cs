using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{
//ATTRIBUTES
    [HideInInspector] public PhysicsComponent PhysComp;
    [HideInInspector] public RayCasterCapsule RayCaster;
    [HideInInspector] public MeshRenderer Renderer;
    [HideInInspector] public ControllerInput controllerInput;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float  strafeMultiplier;
    [SerializeField] private float kineticBatterySlideMagnitude;

    private Checkpoint checkPoint;
    private float skinWidth;

    //dash related attributes
    public Timer dashCooldownTimer;
    public Vector3 lastDash;
    private bool neutralizeInput = false;

    //kinetic battery related attributes
    public int kineticTimer;
    public int divideValue;
    private bool stopKineticSlide;
    private bool currentlySliding;
    private Vector3 oldVelocity = Vector3.zero;
    public Timer kineticBatteryCooldownTimer;

    //GemPrefab
    [SerializeField] private GameObject gemPrefab;

//METHODS
   protected override void Awake()
    {
        RayCaster = GetComponent<RayCasterCapsule>();
        PhysComp = GetComponent<PhysicsComponent>();
        Renderer = GetComponent<MeshRenderer>();
        controllerInput = GameObject.FindObjectOfType<ControllerInput>() as ControllerInput;
        SaveManager._instance.SetGem(gemPrefab);

        skinWidth = PhysComp.GetSkinWidth();

        stopKineticSlide = false;
        currentlySliding = false;

        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        dashCooldownTimer.SubtractTime();
        kineticBatteryCooldownTimer.SubtractTime();
    }

    public Vector3 ProcessInput()
    {       
        RaycastHit hit = RayCaster.GetCollisionData(Vector3.down, skinWidth * 2);
        float verticalInput = controllerInput.GetVerticalInput();
        float horizontalInput = controllerInput.GetHorizontalInput();

        Vector3 input = new Vector3(horizontalInput * strafeMultiplier, 0, verticalInput);
        input = UnityEngine.Camera.main.transform.rotation * input.normalized;

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

    int called = 1;

    public void DecreaseVelocity()
    {
        Debug.Log("call " + called);
        called++;
        if (kineticTimer > 0)
        {
            PhysComp.SetDirection(Vector3.zero);
            Vector3 NewVelocity = PhysComp.GetVelocity() - oldVelocity / oldVelocity.magnitude * kineticBatterySlideMagnitude; PhysComp.SetVelocity(NewVelocity);
            //Vector3 NewVelocity = PhysComp.GetVelocity() - oldVelocity.normalized * kineticBatterySlideMagnitude;
            PhysComp.SetVelocity(NewVelocity);
            kineticTimer -= 1;
        }

        if (PhysComp.GetVelocity().magnitude < 0.2f && GetStopKineticSlide() == false || kineticTimer == -1000 && GetStopKineticSlide() == false || Mathf.Sign(PhysComp.GetVelocity().x) != Mathf.Sign(oldVelocity.x) && Mathf.Sign(PhysComp.GetVelocity().z) != Mathf.Sign(oldVelocity.z))
        {
            Debug.Log("SET ZERO");
            SetStopKineticSlide(true);
            SetCurrentlySliding(true);
            kineticTimer = 0;
            CancelInvoke("DecreaseVelocity");
        }
    }

    private void ReleaseKineticBattery()
    {
        SetStopKineticSlide(true);
        SetCurrentlySliding(true);
        kineticTimer = 0;
        CancelInvoke("DecreaseVelocity");
    }

    
    private bool hasStoppedSliding()
    {
        if (PhysComp.GetVelocity().magnitude < 0.2f && GetStopKineticSlide() == false || kineticTimer == -1000 && GetStopKineticSlide() == false || Mathf.Sign(PhysComp.GetVelocity().x) != Mathf.Sign(oldVelocity.x) && Mathf.Sign(PhysComp.GetVelocity().z) != Mathf.Sign(oldVelocity.z))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

//GETTERS AND SETTERS
    public Vector3 GetCachedVelocity()
    {
        return oldVelocity;
    }
    public void SetCachedVelocity(Vector3 set)
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
    public bool IsMomentumStateActive()
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
    public bool IsPrecisionStateActive()
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
    public void SetStrafeMultiplier(float strafeCoefficient)
    {
        this.strafeMultiplier = strafeCoefficient;
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
