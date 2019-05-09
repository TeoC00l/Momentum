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

    public override void HandleUpdate()
    {


        //Checking for conditions to change state
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            owner.Transition<MomentumAirbourneState>();
        }

        if (owner.physComp.GroundCheck())
        {
            owner.grounded = true;
        }
    }

    public override void HandleFixedUpdate()
    {

        if (owner.grounded)
        {
            owner.Transition<PrecisionState>();

        }

        physComp.AddForces();

        owner.physComp.CollisionCalibration();
    }

}
