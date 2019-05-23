using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathListener : MonoBehaviour
{
    //Attributes
    Drone2[] drones;

    //Methods
    void Start()
    {
        drones = FindObjectsOfType(typeof(Drone2)) as Drone2[];

        EventSystem.Current.RegisterListener(EVENT_TYPE.PLAYER_DIED, OnPlayerDied);
        EventSystem.Current.RegisterListener(EVENT_TYPE.PLAYER_DIED, ResetDrones);
    }

    void OnPlayerDied (EventInfo eventInfo)
    {
        DieEvent unitDeathEventInfo = (DieEvent)eventInfo;
        Debug.Log("Alerted about unit death: " + unitDeathEventInfo.UnitGameObject.name);
        unitDeathEventInfo.UnitGameObject.GetComponent<Health>().Respawn();
    }

    void ResetDrones(EventInfo eventInfo)
    {
        foreach (Drone2 drone in drones)
        {
            drone.Respawn();
        }
    }

}

