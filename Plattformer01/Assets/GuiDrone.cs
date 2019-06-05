using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiDrone : MonoBehaviour
{
    private Drone[] drones;
    private GameObject player;
    [SerializeField]private float GUIToggleDistance;
    [SerializeField]private LayerMask visionMask;
    [SerializeField] private GameObject Gui;
    private bool breakLoop = false;

    // Start is called before the first frame update
    void Awake()
    {
        drones = FindObjectsOfType(typeof(Drone)) as Drone[];
        player = GameObject.FindWithTag("Player");
        
        Gui.SetActive(false);
    }

    private void FixedUpdate()
    {
        if(drones.Length == 0)
        {
            drones = FindObjectsOfType(typeof(Drone)) as Drone[];
        }

        Gui.SetActive(false);

        foreach (Drone D in drones)
        {
            if(D == null)
            {
                drones = FindObjectsOfType(typeof(Drone)) as Drone[];
                break;
            }
            if (D.GetCurrentState().name == "Drone Pursuit State(Clone)")
            {
                Look(D.gameObject);
                if(breakLoop == true)
                {
                    breakLoop = false;
                    break;
                }
            }
        }
    }

    private void Look(GameObject lookAt)
    {
        Vector3 directionToTarget = player.transform.position - lookAt.transform.position;
        float angle = Vector3.Angle(player.transform.forward, directionToTarget);

        if (Mathf.Abs(angle) < 90)
        {
            if (ScanForPlayer(GUIToggleDistance, lookAt) == true)
            {
                Gui.SetActive(true);
                breakLoop = true;
            }

        }
        else
        {
            Gui.SetActive(false);
        }
    }
    private bool ScanForPlayer(float scanDistance, GameObject lookAt)
    {
        Vector3 direction = player.gameObject.transform.position - lookAt.transform.position;
        return Physics.Raycast(lookAt.transform.position, direction.normalized, scanDistance, visionMask);
    }

}
