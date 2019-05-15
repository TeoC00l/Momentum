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
        //Making adjustments to physics
        physComp.AddForces();
        owner.physComp.CollisionCalibration();
    }

    public override void HandleUpdate()
    {
        //Checking for conditions to change state
        if (owner.physComp.GroundCheck() == true)
        {
            owner.Transition<PrecisionState>();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            owner.Transition<MomentumAirbourneState>();
        }


    }

}
