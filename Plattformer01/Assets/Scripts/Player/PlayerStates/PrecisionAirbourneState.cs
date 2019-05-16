using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/PrecisionAirbourneState")]
public class PrecisionAirbourneState : PrecisionState
{
    //Methods
    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleFixedUpdate()
    {
        //Checking for conditions to change state
        if (owner.physComp.GroundCheck() == true)
        {
            owner.Transition<PrecisionState>();
        }

        //Making adjustments to physics
        owner.AddPhysics();
        owner.physComp.CollisionCalibration();
    }

    public override void HandleUpdate()
    {
        //Checking for conditions to change state
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            owner.Transition<MomentumAirbourneState>();
        }


    }

}
