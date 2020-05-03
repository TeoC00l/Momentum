using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/BaseState")]
public class PlayerBaseState : State
{
    //Attributes

    protected PhysicsComponent PhysicsComponent;
    protected RayCasterCapsule rayCaster;
    protected Player owner;
    protected int kineticTimer;

    //Methods
    public override void Enter()
    {
        kineticTimer = owner.kineticTimer;
        PhysicsComponent = owner.PhysComp;
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
