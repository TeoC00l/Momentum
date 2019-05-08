using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsorBaseState : State
{

    protected Repulsor owner;
    //Attributes


    //Methods

    public override void Initialize(StateMachine owner)
    {
        this.owner = (Repulsor)owner;
    }
}
