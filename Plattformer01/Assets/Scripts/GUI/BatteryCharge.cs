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
    public float growFactor = 0.2f;
    private float Timer = 1000; //this is the current cooldown of kinetic battery
    private RectTransform rectTransform;
    private Player playerScript;

    //fixTimeText


    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");  //all players should be called Character
        chargeBar = GameObject.Find("chargeGUI");
        rectTransform = chargeBar.GetComponent<RectTransform>();
        playerScript = player.GetComponent<Player>();
        //set to 0 at start 
        rectTransform.localScale = new Vector2(0f,0f); // width and height
    }

    void Update()
    {
        if (playerScript.GetKineticActive())
        {
            CancelInvoke("DecreaseBar");
            //countdown
            InvokeRepeating("IncreaseBar", 0f, 0.05f);
            
            if (FullChargeSize < rectTransform.localScale.x)
            {
                CancelInvoke("IncreaseBar");
;           }
               
            

        }
        else
        {
           
            CancelInvoke("IncreaseBar");
            // rectTransform.localScale = new Vector2(0f, 0f);
            if (minChargeSize < rectTransform.localScale.x)
            {
                InvokeRepeating("DecreaseBar", 0f, 0.05f);
            }
            else
            {
                rectTransform.localScale = new Vector3(0f, 0.62f, 0);

                CancelInvoke("DecreaseBar");

            }
            


        }
        


    }
    private void IncreaseBar()
    {
        
            Debug.Log("KineticActive2");
        rectTransform.localScale = new Vector3(rectTransform.localScale.x, 0.62f, 0);
        rectTransform.localScale += new Vector3(growFactor, 0f, 0);
        
    }
    private void DecreaseBar()
    {

        Debug.Log("KineticActive2");
        rectTransform.localScale = new Vector3(rectTransform.localScale.x, 0.62f, 0);
        rectTransform.localScale -= new Vector3(growFactor, 0f, 0);

    }
}
