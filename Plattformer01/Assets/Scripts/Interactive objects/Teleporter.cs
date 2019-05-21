using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Transform locationOut;
    [SerializeField] private DroneParent droneParent;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("PlayerTeleported dawg");
            droneParent.ResetDrones();
            player.GetComponent<PhysicsComponent>().SetVelocity(Vector3.zero);
            player.GetComponent<Transform>().rotation = locationOut.rotation;
            player.GetComponent<Transform>().position = locationOut.position;
            //@Teo
            //insert script in player to reset player's orientation when they respawn.
            //otherwise they respawn facing backwards
            //which is awkward.
        }   
    }
}
