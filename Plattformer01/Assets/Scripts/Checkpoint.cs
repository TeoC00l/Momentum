﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //Attributes
    [SerializeField] private float playerYAxisRotation;

    private Health playerHealth;
    private GameObject player;
    private ControllerInput controllerInput;
    
    //Methods
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        controllerInput = GameObject.FindObjectOfType<ControllerInput>() as ControllerInput;
    }

    public void SetPlayerPositionHere()
    {
        player.transform.position = gameObject.transform.position;
        controllerInput.SetRotationY(playerYAxisRotation);
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player") && playerHealth.GetCheckPoint().gameObject != this.gameObject)
        {
            Destroy(playerHealth.GetCheckPoint().gameObject);
            Destroy(gameObject.GetComponent<BoxCollider>());
            playerHealth.SetCheckPoint(this);
            SaveManager._instance.SaveGame();

            Debug.Log("checkpoint reached: " + this.name);
        }
    }
}
