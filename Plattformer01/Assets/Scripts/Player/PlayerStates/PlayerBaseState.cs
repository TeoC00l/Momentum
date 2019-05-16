using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/BaseState")]
public class PlayerBaseState : State
{
    //Attributes
    [SerializeField] protected Material material;
    [SerializeField] protected PhysicsComponent physComp;
    [SerializeField] protected RayCasterCapsule rayCaster;
    protected Player owner;
    public float batteryTimer;

    //Methods
    public override void Enter()
    {
        physComp = owner.physComp;
        owner.Renderer.material = material;
        rayCaster = owner.rayCaster;
        
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
