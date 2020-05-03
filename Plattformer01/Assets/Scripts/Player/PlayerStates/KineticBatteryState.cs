//Author: Teodor Tysklind
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/KineticBatteryState")]
public class KineticBatteryState : PlayerBaseState
{
//ATTRIBUTES
    private bool exitingState;

    [Header("Slow down the kinetic slide every X seconds")]
    [SerializeField]private float slideDecreaseVelocityInterval;

    [Header("Cooldown for using kinetic battery")]
    [SerializeField]private int kineticBatteryCooldown;

//METHODS
    public override void Enter()
    {
        base.Enter();

        if (owner.GetCachedVelocity() == Vector3.zero)
        {
            owner.SetCachedVelocity(PhysicsComponent.GetVelocity());
        }

        if (owner.GetCurrentlySliding() == false)
        {
            owner.SetStopKineticSlide(false);
            kineticTimer = kineticBatteryCooldown;
            owner.InvokeRepeating("DecreaseVelocity", 0, slideDecreaseVelocityInterval);
        }
    }

    public override void HandleFixedUpdate()
    {
        if (owner.GetStopKineticSlide() == false)
        {
            owner.PhysComp.AddForces();
        }

        if (PhysicsComponent.GetVelocity() == Vector3.zero && owner.GetStopKineticSlide() == false)
        {
            PhysicsComponent.SetVelocity(Vector3.zero);
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
            exitingState = owner.controllerInput.GetIsKineticBatteryActive() == false;
            if (exitingState == true)
            {
                owner.SetCurrentlySliding(false);
                owner.SetStopKineticSlide(false);

                exitingState = false;

                Vector3 input = owner.transform.forward;
                PhysicsComponent.SetVelocity(input * owner.GetCachedVelocity().magnitude);
                ProperlyExitState();
                owner.SetCachedVelocity(Vector3.zero);
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
    }
   

}
