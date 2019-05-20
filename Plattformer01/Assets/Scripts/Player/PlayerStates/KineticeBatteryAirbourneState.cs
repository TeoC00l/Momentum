using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Player/KineticBatteryAirbourneState")]
public class KineticeBatteryAirbourneState : PlayerBaseState
{
    Vector3 input = new Vector3(1, 0, 1);

    bool StopOnce;
    bool getOutofState;
    [SerializeField] private float slideDecreaseMovementRate;
    [SerializeField] private float waitBeforeSliding;
    void Awake()
    {
        
    }
    public override void Enter()
    {
        base.Enter();
        owner.kineticBatteryActive = true;
        if(owner.GetOldVelocity() == Vector3.zero)
        {
            owner.SetOldVelocity(PhysComp.GetVelocity());
        }

        if (owner.GetKineticActive() == false)
        {
            StopOnce = false;
            Debug.Log("startSlide");
            owner.kineticTimer = 1000;
            owner.divideValue = owner.kineticTimer;
            owner.InvokeRepeating("DecreaseVelocity", waitBeforeSliding, slideDecreaseMovementRate);
        }
        owner.SetKineticActive(true);
    }

    public override void HandleFixedUpdate()
    {
        if (StopOnce == false)
        {
            owner.PhysComp.AddForces();
            owner.PhysComp.CollisionCalibration();
        }

        if (PhysComp.GetVelocity() == Vector3.zero && StopOnce == false)
        {
            StopOnce = true;
            owner.CancelInvoke("DecreaseVelocity");
        }
        if (owner.PhysComp.GroundCheck() == true)
        {
            owner.Transition<KineticBatteryState>();
        }
       
    }
    public override void HandleUpdate()
    {
        getOutofState = Input.GetMouseButtonDown(0);
        if (getOutofState == true)
        {
            getOutofState = false;
            input = owner.transform.forward;
            PhysComp.SetVelocity(input * owner.GetOldVelocity().magnitude);
            ProperlyExitState();
            owner.SetKineticActive(false);
            owner.SetOldVelocity(Vector3.zero);
            owner.Transition<MomentumAirbourneState>();
        }

    }
    public override void Exit()
    {
        owner.kineticBatteryActive = false;
        owner.kineticBatteryCooldownTimer.SetTimer();
    }
    private void ProperlyExitState()
    {
        owner.CancelInvoke("DecreaseVelocity");
        owner.kineticTimer = 0;
        owner.AddPhysics();
        owner.PhysComp.CollisionCalibration();
    }
}

