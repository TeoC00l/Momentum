using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryCharge : MonoBehaviour
{
    private GameObject player; //player
    [SerializeField]private GameObject kineticGraphics; //the grapghics for kinetic battery
    [SerializeField] private GameObject kineticArrow;
    private GameObject chargeBar; //the chargebar
    private float FullChargeSize = 0.81f; //where the battery tops off
    private float minChargeSize = 0; //where the battery stops
    private float growFactor = 0.1f;
    private RectTransform rectTransform;
    private Player playerScript;
    private bool stopCharge;

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
            kineticArrow.SetActive(true);
            if (FullChargeSize > rectTransform.localScale.x && stopCharge == false)
            {
                stopCharge = false;
                InvokeRepeating("IncreaseBar", 0f, 0.1f);

;
            }
            else
            {
                stopCharge = true;
                rectTransform.localScale = new Vector3(FullChargeSize, 0.62f, 0);

                CancelInvoke("IncreaseBar");

            }



        }
        else
        {
            kineticArrow.SetActive(false);
            if (minChargeSize < rectTransform.localScale.x && stopCharge == true)
            {
                stopCharge = true;
                InvokeRepeating("DecreaseBar", 0f, 0.1f);
            }
            else
            {
                stopCharge = false;
               rectTransform.localScale = new Vector3(minChargeSize, 0.62f, 0);

                CancelInvoke("DecreaseBar");

            }
            


        }
        


    }
    private void IncreaseBar()
    {
        
        rectTransform.localScale = new Vector3(rectTransform.localScale.x, 0.62f, 0);
        if(FullChargeSize < rectTransform.localScale.x )
        {
            rectTransform.localScale = new Vector3(FullChargeSize, 0.62f, 0);

            stopCharge = true;
        }
        else
        {
            rectTransform.localScale += new Vector3(growFactor, 0f, 0);

        }

    }
    private void DecreaseBar()
    {


        rectTransform.localScale = new Vector3(rectTransform.localScale.x, 0.62f, 0);
        rectTransform.localScale -= new Vector3(growFactor, 0f, 0);

    }
}
