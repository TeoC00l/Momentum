﻿using System.Collections;
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
    public GameObject checkPoint;
    public GameObject spawnPoint;
    public float detectionDistance;

    //Methods
    protected override void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        base.Awake();
    }

    void OnTriggerEnter(Collider other)
    {
        player.transform.position = checkPoint.transform.position;
        player.physComp.velocity = Vector3.zero;

        transform.position = spawnPoint.transform.position;

        Transition<DroneIdleState>();
    }

    public bool CheckForPlayer()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, player.transform.position, out hit, detectionDistance, visionMask);

        if (hit.collider != null)
            {
            Debug.Log("poop");
            return true;
            }
            else
            {
                return false;
            }
    }
}
