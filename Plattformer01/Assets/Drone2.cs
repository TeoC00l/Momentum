using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Drone2: StateMachine
{
    //Attributes
    [HideInInspector] public MeshRenderer Renderer;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    public LayerMask visionMask;
    public Player player;

    //Methods
    protected override void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        base.Awake();
    }

    protected override void Update()
    {
        navMeshAgent.destination = player.transform.position;
    }


}
