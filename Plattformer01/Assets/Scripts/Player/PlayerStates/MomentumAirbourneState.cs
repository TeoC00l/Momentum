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
        //Checking for conditions to change state
        if (owner.PhysComp.GroundCheck() == true)
        {
            owner.Transition<MomentumState>();
        }

        //Making adjustments to physics
        owner.AddPhysics();
        owner.PhysComp.CollisionCalibration();
    }


    public override void HandleUpdate()
    {
        //Checking for conditions to change state
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            owner.Transition<PrecisionAirbourneState>();
        }

    }
}