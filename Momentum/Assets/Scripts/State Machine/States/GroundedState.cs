using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerGroundedState")]
public class GroundedState : PlayerBaseState
{
    //Attributes
    State airBourne;

    //Methods

    public override void Enter()
    {
        base.Enter();
        airBourne = owner.GetComponent<AirbourneState>();
    }

    public override void HandleUpdate()
    {
        if (!owner.physComp.GroundCheck())
        {

        }

        owner.AddPhysics();

        if (Input.GetKeyDown("space"))
        {
            owner.physComp.Jump();
        }

        owner.physComp.CollisionCalibration();
    }
}
