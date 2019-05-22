using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathListener : MonoBehaviour
{
    //Attributes
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawn;

    //Methods
    void Start()
    {
        EventSystem.Current.RegisterListener(EVENT_TYPE.UNIT_DIED, OnUnitDied);
    }

    void OnUnitDied (EventInfo eventInfo)
    {
        DieEvent unitDeathEventInfo = (DieEvent)eventInfo;
        Debug.Log("Alerted about unit death: " + unitDeathEventInfo.UnitGameObject.name);
        Instantiate(player, spawn.transform);
    }
}

