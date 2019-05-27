using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawn : MonoBehaviour
{
    //Attributes
    private Vector3 pointInSpace;
    private GameObject activeDrone;
    [SerializeField] private GameObject dronePrefab;


    //Methods
    void Start()
    {
        Spawn();
    }

    public void Respawn()
    {
        Destroy(activeDrone);
        Spawn();
    }

    void Spawn()
    {
        activeDrone = Instantiate(dronePrefab, transform.position, transform.rotation);
    }
}
