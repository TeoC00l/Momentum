using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/KineticBatteryState")]
public class KineticBatteryState : PlayerBaseState
{
    //Attributes
    Vector3 returnVelocity;
    Vector3 input = new Vector3(1,0,1);

    bool StopOnce;
    bool getOutofState;
    [SerializeField]private float slideDecreaseMovementRate;
    [SerializeField]private float waitBeforeSliding;
    //Methods
    public override void Enter()
    {
        base.Enter();
        owner.kineticBatteryActive = true;
        owner.oldVelocity = physComp.velocity;
        returnVelocity = physComp.velocity;
        StopOnce = false;
        owner.kineticTimer = 10000;
        owner.divideValue = owner.kineticTimer;
        owner.InvokeRepeating("DecreaseVelocity", waitBeforeSliding, slideDecreaseMovementRate);
    }

    public override void HandleFixedUpdate()
    {
        if (StopOnce == false)
        {
            owner.physComp.AddForces();
            owner.physComp.CollisionCalibration();
        }

        if (physComp.velocity == Vector3.zero && StopOnce == false)
        {
            StopOnce = true;
            owner.CancelInvoke("DecreaseVelocity");
        }
        if (owner.physComp.GroundCheck() == false)
        {
            ProperlyExitState();
            owner.Transition<MomentumAirbourneState>();
        }
        if (getOutofState == true)
        {
            getOutofState = false;
            physComp.SetDirection(owner.ProcessVerticalInput() + (owner.ProcessHorizontalInput()));
            physComp.velocity = physComp.direction * returnVelocity.magnitude;
            ProperlyExitState();
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
        owner.kineticBatteryActive = false;
        owner.kineticBatteryCooldownTimer.SetTimer();
    }
    private void ProperlyExitState()
    {
        owner.CancelInvoke("DecreaseVelocity");
        owner.kineticTimer = 0;
        owner.AddPhysics();
        owner.physComp.CollisionCalibration();
    }
   

}
