using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryCharge : MonoBehaviour
{
    public GameObject player; //player
    public GameObject kineticGraphics; //the grapghics for kinetic battery
    public GameObject chargeBar; //the chargebar
    public float FullChargeSize = 0.81f; //where the battery tops off
    public float minChargeSize = 0; //where the battery stops
    public float growFactor = 50;
    private float Timer = 1000; //this is the current cooldown of kinetic battery

    //fixTimeText


    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Character");  //all players should be called Character
        chargeBar = GameObject.Find("chargeBar");

        //set to 0 at start 
        chargeBar.GetComponent<RectTransform>().sizeDelta = new Vector3(0,0,0); // width and height
    }

    void Update()
    {
        if (player.GetComponent<Player>().GetKineticBatteryActive())
        {
            //countdown
            while (FullChargeSize > chargeBar.transform.localScale.y)
            {
                Timer += Time.deltaTime;
                chargeBar.GetComponent<RectTransform>().sizeDelta = new Vector3(minChargeSize + growFactor, minChargeSize+growFactor,0);
            }
        }

    }

}
