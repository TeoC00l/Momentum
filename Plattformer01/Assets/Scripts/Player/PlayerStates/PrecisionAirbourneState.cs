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
        if (owner.physicsComponent.GroundCheck() == true)
        {
            owner.Transition<PrecisionState>();
        }

        //Making adjustments to physics
        owner.AddPhysics();
        //owner.PhysComp.AddNormalForces();
    }

    public override void HandleUpdate()
    {
        //Checking for conditions to change state
        if (owner.controllerInput.GetIsPrecisionModeActive() == false)
        {
            owner.Transition<MomentumAirbourneState>();
        }
    }
}
