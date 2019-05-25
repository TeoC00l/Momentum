using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathListener : MonoBehaviour
{
    //Attributes
    private DroneSpawn[] droneSpawn;

    //Methods
    void Start()
    {
        droneSpawn = FindObjectsOfType(typeof(DroneSpawn)) as DroneSpawn[];

        EventSystem.Current.RegisterListener(EVENT_TYPE.PLAYER_DIED, RespawnPlayer);
        EventSystem.Current.RegisterListener(EVENT_TYPE.PLAYER_DIED, ResetDrones);
    }

    void RespawnPlayer (EventInfo eventInfo)
    {
        DieEvent unitDeathEventInfo = (DieEvent)eventInfo;
        GameObject player = unitDeathEventInfo.UnitGameObject;

        player.GetComponent<Health>().Respawn();
        player.GetComponent<Player>().Transition<RespawnState>();

        Debug.Log("Alerted about unit death: " + player.name);
    }

    void ResetDrones(EventInfo eventInfo)
    {
        foreach (DroneSpawn drone in droneSpawn)
        {
            drone.Respawn();
        }
    }

    void ResetDestructibleObjects()
    {
        //TODO
    }

}

