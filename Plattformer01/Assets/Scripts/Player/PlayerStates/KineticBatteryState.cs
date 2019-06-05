﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/KineticBatteryState")]
public class KineticBatteryState : PlayerBaseState
{
    //Attributes
    Vector3 input = new Vector3(1,0,1);

    
    bool getOutofState;
    [SerializeField]private float slideDecreaseMovementRate;
    [SerializeField]private float waitBeforeSliding;
    //Methods
    public override void Enter()
    {
        base.Enter();
        if (owner.GetOldVelocity() == Vector3.zero)
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
            owner.PhysComp.CollisionCalibration();
        }
        if (PhysComp.GetVelocity() == Vector3.zero && owner.GetStopKineticSlide() == false)
        {
            PhysComp.SetVelocity(Vector3.zero);
            owner.SetStopKineticSlide(true);
            owner.CancelInvoke("DecreaseVelocity");
        }

        if ( owner.GetStopKineticSlide() == true)
        {
            owner.CancelInvoke("DecreaseVelocity");
        }
        if (owner.PhysComp.GroundCheck() == false)
        {
            owner.SetCurrentlySliding(true);

            owner.Transition<KineticBatteryAirbourneState>();
        }
       
    }

    public override void HandleUpdate()
    {
        if (Time.timeScale == 1)
        {
            //Redirecting velocity
            getOutofState = owner.controllerInput.GetIsKineticBatteryActive() == false;
            if (getOutofState == true)
            {
                owner.SetCurrentlySliding(false);
                owner.SetStopKineticSlide(false);

                getOutofState = false;

                input = owner.transform.forward;
                PhysComp.SetVelocity(input * owner.GetOldVelocity().magnitude);
                ProperlyExitState();
                owner.SetOldVelocity(Vector3.zero);
                owner.Transition<MomentumState>();
            }
        }

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
