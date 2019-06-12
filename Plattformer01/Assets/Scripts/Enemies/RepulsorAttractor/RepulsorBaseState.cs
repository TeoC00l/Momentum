﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsorBaseState : State
{
    //Attributes
    protected RepulsorAttractor owner;

    //Methods
    public override void Initialize(StateMachine owner)
    {
        this.owner = (RepulsorAttractor)owner;
    }
}
