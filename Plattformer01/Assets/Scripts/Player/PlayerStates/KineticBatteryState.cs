using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/KineticBatteryState")]
public class KineticBatteryState : PlayerBaseState
{
    //Attributes
    Vector3 input = new Vector3(1,0,1);

    private bool stopOnce;
    private bool getOutofState;
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
       
        if (owner.GetKineticActive() == false)
        {
            stopOnce = false;
            Debug.Log("startSlide");

            owner.SetKineticCounter(1000);
            owner.SetKineticSlideDivideValue(owner.GetKineticCounter());
            owner.InvokeRepeating("DecreaseVelocity", waitBeforeSliding, slideDecreaseMovementRate);
        }
        owner.SetKineticActive(true);
    }

    public override void HandleFixedUpdate()
    {
        if (stopOnce == false)
        {
            owner.PhysComp.AddForces();
            owner.PhysComp.CollisionCalibration();
        }

        if (PhysComp.GetVelocity() == Vector3.zero && stopOnce == false)
        {
            stopOnce = true;
            owner.CancelInvoke("DecreaseVelocity");
        }
        if (owner.PhysComp.GroundCheck() == false)
        {
            owner.Transition<KineticeBatteryAirbourneState>();
        }
        if (getOutofState == true)
        {
            getOutofState = false;
            
            input = owner.transform.forward;
            PhysComp.SetVelocity(input * owner.GetOldVelocity().magnitude);
            ProperlyExitState();
            owner.SetKineticActive(false);
            owner.SetOldVelocity(Vector3.zero);
            owner.Transition<MomentumState>();
        }
    }

    public override void HandleUpdate()
    {
        //Redirecting velocity
        getOutofState = Input.GetMouseButtonDown(0);

    }

    public override void Exit()
    {
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
