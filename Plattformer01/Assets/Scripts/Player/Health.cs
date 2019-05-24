using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Attributes
    private Checkpoint checkPoint;

    //Methods
    public void Start()
    {
        checkPoint = GameObject.Find("StartPoint").GetComponent<Checkpoint>();
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
        checkPoint.SetPlayerPositionHere();
    }

    //GETTERS AND SETTERS
    public void SetCheckPoint(Checkpoint checkPoint)
    {
        this.checkPoint = checkPoint;
    }
    public Checkpoint GetCheckPoint()
    {
        return checkPoint;
    }
}

