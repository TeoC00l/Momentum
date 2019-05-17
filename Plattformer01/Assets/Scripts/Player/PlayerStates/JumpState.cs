using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/JumpState")]
public class JumpState : PlayerBaseState
{
    //Attributes

    //Methods
    public override void Enter()
    {

    }

    public override void HandleFixedUpdate()
    {
        owner.PhysComp.Jump();
        owner.TransitionBack();
    }

}
