using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/MomentumAirbourneState")]
public class MomentumAirbourneState : MomentumState
{
    //Methods
    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleFixedUpdate()
    {
        //Making adjustments to physics
        physComp.AddForces();
        owner.physComp.CollisionCalibration();
    }


    public override void HandleUpdate()
    {
        //Checking for conditions to change state
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            owner.Transition<PrecisionAirbourneState>();
        }

        if (owner.physComp.GroundCheck() == true)
        {
            owner.Transition<MomentumState>();
        }


    }
}