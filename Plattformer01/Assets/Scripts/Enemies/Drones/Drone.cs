using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Drone : StateMachine
{
    //Attributes
    [HideInInspector] public MeshRenderer Renderer;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    public LayerMask visionMask;
    public Player player;
    public float detectionDistance;
    public float GUIToggleDistance;

    private Health playerHealth;

    //Methods
    protected override void Awake()
    {
   //     GUI = GameObject.FindGameObjectWithTag("DroneGUI");
        player = FindObjectOfType(typeof(Player)) as Player;
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerHealth = player.GetComponent<Health>();
   //     ToggleGUI(false);

        base.Awake();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player was killed by" + gameObject.name);

            playerHealth.Die();
        }
    }

    public void ToggleGUI(bool isActive)
    {
     //   GUI.SetActive(isActive);      
    }
}
