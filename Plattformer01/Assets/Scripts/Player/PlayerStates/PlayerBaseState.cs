using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/BaseState")]
public class PlayerBaseState : State
{
    //Attributes
    [SerializeField] protected Material material;

    protected PhysicsComponent PhysComp;
    protected RayCasterCapsule rayCaster;
    protected Player owner;
    

    //Methods
    public override void Enter()
    {
        PhysComp = owner.PhysComp;
        owner.Renderer.material = material;
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
