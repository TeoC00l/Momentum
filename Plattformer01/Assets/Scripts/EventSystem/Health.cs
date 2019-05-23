using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private GameObject checkPoint;

    public void Start()
    {
        checkPoint = GameObject.Find("StartPoint");
    }

    public void Die()
    {
        DieEvent udei = new DieEvent();
        udei.EventDescription = "Unit " + gameObject.name + " has died.";
        udei.UnitGameObject = gameObject;

        EventSystem.Current.FireEvent(EVENT_TYPE.PLAYER_DIED, udei);
    }

    public void Respawn()
    {
        gameObject.transform.position = checkPoint.transform.position;
    }
}

