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
    public GameObject spawnPoint;
    public float detectionDistance;

    private Health playerHealth;

    //Methods
    protected override void Awake()
    {
        player = FindObjectOfType(typeof(Player)) as Player;
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerHealth = player.GetComponent<Health>();
        base.Awake();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player was killed by" + gameObject.name);
            navMeshAgent.ResetPath();
            playerHealth.Die();
        }
    }

    public void Respawn()
    {
        gameObject.transform.position = spawnPoint.transform.position;
        TransitionBack();
    }
}
