using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private GameObject player;
    public Transform locationOut;
    public DroneParent droneParent;

    void Awake()
    {
        player = GameObject.Find("Character");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            droneParent.ResetDrones();
            player.GetComponent<PhysicsComponent>().SetVelocity(Vector3.zero);
            player.GetComponent<Transform>().position = locationOut.position;
            //@Teo
            //insert script in player to reset player's orientation when they respawn.
            //otherwise they respawn facing backwards
            //which is awkward.
        }   
    }
}
