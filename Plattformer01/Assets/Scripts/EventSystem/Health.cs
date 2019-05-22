using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Die();
        }
    }

    void Die()
    {
        DieEvent udei = new DieEvent();
        udei.EventDescription = "Unit " + gameObject.name + " has died.";
        udei.UnitGameObject = gameObject;

        EventSystem.Current.FireEvent(EVENT_TYPE.UNIT_DIED, udei);

        Destroy(gameObject);
    }
}

