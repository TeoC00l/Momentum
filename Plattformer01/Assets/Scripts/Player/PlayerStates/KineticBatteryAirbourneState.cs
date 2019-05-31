using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Player/KineticBatteryAirbourneState")]
public class KineticBatteryAirbourneState : PlayerBaseState
{
    Vector3 input = new Vector3(1, 0, 1);

    bool getOutofState;
    public float gravitationalForce = 10f;

    private Vector3 zeroExceptForGravity;
    [SerializeField] private float slideDecreaseMovementRate;
    [SerializeField] private float waitBeforeSliding;
 
    public override void Enter()
    {
        base.Enter();
        if(owner.GetOldVelocity() == Vector3.zero)
        {
            owner.SetOldVelocity(PhysComp.GetVelocity());
        }

        if (owner.GetCurrentlySliding() == false)
        {
            owner.SetStopKineticSlide(false);
            Debug.Log("startSlide");
            owner.kineticTimer = 1000;
            owner.divideValue = owner.kineticTimer;
            owner.InvokeRepeating("DecreaseVelocity", waitBeforeSliding, slideDecreaseMovementRate);
        }

    }

    public override void HandleFixedUpdate()
    {

        if (owner.GetStopKineticSlide() == false)
        {
            owner.PhysComp.AddForces();
        }

        if (PhysComp.GetVelocity() == Vector3.zero && owner.GetStopKineticSlide() == false)
        {
            PhysComp.SetVelocity(Vector3.zero);
            owner.SetStopKineticSlide(true);
            owner.CancelInvoke("DecreaseVelocity");
        }
        if (owner.PhysComp.GroundCheck() == true)
        {
            owner.SetCurrentlySliding(true);

            owner.Transition<KineticBatteryState>();
        }else if(owner.GetStopKineticSlide() == true)
        {
            owner.PhysComp.AddGravity();

        }
        owner.PhysComp.CollisionCalibration();

    }
    public override void HandleUpdate()
    {
        

    }
    public override void Exit()
    {
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

