using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abyss2 : MonoBehaviour
{
    //Attributes
    private GameObject player;
    private Health health;

    private void Start()
    {
        //player = GameObject.FindWithTag("Player");
        //health = GameObject.FindWithTag("Player");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player)
        {
            //player.Health.Die();
            //Destroy(player);
        }
    }
}
