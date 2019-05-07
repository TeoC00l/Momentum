using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Drone/BaseState")]
public class DroneBaseState : State
{
    //Attributes
    protected Drone2 owner;
    public float detectionDistance;

    //Methods
    public override void Enter()
    {

    }

    public override void Initialize(StateMachine owner)
    {
        this.owner = (Drone2)owner;
    }
}
