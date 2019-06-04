using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiDrone : MonoBehaviour
{
    private DroneSpawn[] droneSpawn;
    private GameObject player;
    private Image image;
    [SerializeField]private float GUIToggleDistance;
    [SerializeField]private LayerMask visionMask;
    [SerializeField] private GameObject Gui;



    // Start is called before the first frame update
    void Awake()
    {
        droneSpawn = FindObjectsOfType(typeof(DroneSpawn)) as DroneSpawn[];
        player = GameObject.FindWithTag("Player");
        Image image = GetComponent<Image>();
          Debug.Log("we fine");
        
        Gui.SetActive(false);
        

    }
    private void Update()
    {
        if(droneSpawn.Length == 0)
        {
            droneSpawn = FindObjectsOfType(typeof(DroneSpawn)) as DroneSpawn[];
        }
        foreach (DroneSpawn D in droneSpawn)
        {
            
                Look(D.GetDrone());

            
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
