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
        if(activeDrone == null)
        {
            Spawn();
        }
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
    public void SpecifikSpawn(Vector3 position,Transform transform)
    {
        if(activeDrone != null)
        {
            Destroy(activeDrone);
        }
        activeDrone = Instantiate(dronePrefab, position, transform.rotation);
        activeDrone.transform.LookAt(transform);

    }
    public GameObject GetDrone()
    {
        return activeDrone;
    }
}
