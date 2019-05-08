using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsorField : MonoBehaviour
{
    //Attributes
    [HideInInspector] public GameObject character;
    [HideInInspector] public PhysicsComponent playerPhysComp;

    public void Start()
    {
        character = GameObject.Find("Character");
        playerPhysComp = character.GetComponent<PhysicsComponent>();
    }
    void OnTriggerEnter(Collider other)
    {
         Debug.Log("MoneyTrees");
         playerPhysComp.velocity = -playerPhysComp.velocity;
    }

}
