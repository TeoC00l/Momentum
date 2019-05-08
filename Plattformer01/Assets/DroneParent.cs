using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneParent : MonoBehaviour
{
    Drone2[] targetList;

    public void Start()
    {
        targetList = GetComponentsInChildren<Drone2>();
    }

    public void ResetDrones()
    {

        foreach (Drone2 drone in targetList)
        {
            Debug.Log("Parent call");
            drone.ResetDronePosition();
        }
    }
}
