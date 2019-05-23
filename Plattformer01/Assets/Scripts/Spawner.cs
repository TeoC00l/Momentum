using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Attributes
    [SerializeField] private GameObject spawnObject;

    //Methods
    public void Spawn()
    {
        spawnObject.transform.position = gameObject.transform.position;
    }
}
