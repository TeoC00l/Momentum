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
    public GameObject checkPoint;
    public GameObject spawnPoint;
    public DroneParent droneParent;
    public float detectionDistance;

    //Methods
    protected override void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        droneParent = GetComponentInParent<DroneParent>();

        base.Awake();
    }

    public void OnTriggerEnter(Collider other)
    {
        navMeshAgent.ResetPath();

        ResetPlayerPosition();
        droneParent.ResetDrones();

    }

    public void ResetDronePosition()
    {
        //navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;
        transform.position = spawnPoint.transform.position;
        Transition<DroneIdleState>();

    }

    public void ResetPlayerPosition()
    {
        player.transform.position = checkPoint.transform.position;

    }
}
