using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/AirbourneState")]
public class AirbourneState : PlayerBaseState
{
    //Methods
    public override void Enter()
    {
        owner.Renderer.material = material;
    }

    public override void HandleUpdate()
    {
        if (owner.physComp.GroundCheck())
        {
            owner.Transition<GroundedState>();
        }

        base.HandleUpdate();
    }
}
