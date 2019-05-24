using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathListener : MonoBehaviour
{
    //Attributes
    private Drone2[] drones;

    //Methods
    void Start()
    {
        drones = FindObjectsOfType(typeof(Drone2)) as Drone2[];

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
        foreach (Drone2 drone in drones)
        {
            drone.Respawn();
        }
    }

    void ResetDestructibleObjects()
    {
        //TODO
    }

}

