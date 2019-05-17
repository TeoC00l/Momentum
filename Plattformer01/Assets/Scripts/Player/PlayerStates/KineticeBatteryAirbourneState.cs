using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Player/KineticBatteryAirbourneState")]
public class KineticeBatteryAirbourneState : PlayerBaseState
{
    Vector3 input = new Vector3(1, 0, 1);

    private bool StopOnce;
    private bool getOutofState;
    [SerializeField] private float slideDecreaseMovementRate;
    [SerializeField] private float waitBeforeSliding;
    void Awake()
    {
        
    }
    public override void Enter()
    {
        base.Enter();
        owner.SetKineticActive(true);
        if(owner.GetOldVelocity() == Vector3.zero)
        {
            owner.SetOldVelocity(PhysComp.GetVelocity());
        }

        if (owner.GetKineticActive() == false)
        {
            StopOnce = false;
            Debug.Log("startSlide");
            owner.SetKineticCounter(1000);
            owner.SetKineticSlideDivideValue(owner.GetKineticCounter());
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
    public override void HandleUpdate()
    {
        getOutofState = Input.GetMouseButtonDown(0);


    }
    public override void Exit()
    {
        owner.SetKineticActive(false);
        owner.kineticBatteryCooldownTimer.SetTimer();
    }
    private void ProperlyExitState()
    {
        owner.CancelInvoke("DecreaseVelocity");
        owner.SetKineticCounter(0);
        owner.AddPhysics();
        owner.PhysComp.CollisionCalibration();
    }
}

