using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsorField : MonoBehaviour
{

    //Attributes
    [HideInInspector] public GameObject character;
    [HideInInspector] public PhysicsComponent playerPhysComp;
    private MeshRenderer forceFieldMesh;

    public void Awake()
    {
        character = GameObject.FindWithTag("Player");
        playerPhysComp = character.GetComponent<PhysicsComponent>();
        forceFieldMesh = GetComponent<MeshRenderer>();
    }

    void OnTriggerEnter(Collider other)
    {
         Debug.Log("MoneyTrees");
         playerPhysComp.velocity = -playerPhysComp.velocity;
    }

}
