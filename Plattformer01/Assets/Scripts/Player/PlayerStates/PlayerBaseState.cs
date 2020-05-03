using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/BaseState")]
public class PlayerBaseState : State
{
    //Attributes

    protected PhysicsComponent physicsComponent;
    protected RayCasterCapsule rayCaster;
    protected Player owner;

    //Methods
    public override void Enter()
    {
        physicsComponent = owner.physicsComponent;
    }

    public override void Initialize(StateMachine owner)
    {
        this.owner = (Player)owner;
    }

    public override void HandleUpdate()
    {
    }

    public override void HandleFixedUpdate()
    {
    }

}
