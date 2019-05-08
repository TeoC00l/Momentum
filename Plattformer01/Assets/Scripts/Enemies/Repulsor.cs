using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repulsor : StateMachine
{
    //Attributes
    [HideInInspector] public MeshRenderer Renderer;
    [HideInInspector] public CapsuleCollider blastCollider;
    [HideInInspector] public GameObject character;
    [HideInInspector] public PhysicsComponent playerPhysComp;
    [SerializeField] public Timer repulsorCooldownTimer;
}
