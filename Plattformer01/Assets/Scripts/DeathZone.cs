using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    //Attributes
    private GameObject player;
    private Health health;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        health = player.GetComponent<Health>();
    }

    void OnTriggerEnter(Collider other)
    {
        health.Die();
    }
}
