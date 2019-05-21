using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashGui : MonoBehaviour
{
    private GameObject player; //player
    private GameObject dashBar; //the chargebar
    private float FullChargeSize = 0.81f; //where the battery tops off
    private float minChargeSize = 0; //where the battery stops
    private float growFactor = 0.2f;
    private Transform GuiDashtransform;
    private Player playerScript;
    private bool stopCharge;
    private bool fullOnce = true;
    [SerializeField]private Timer dashTimer;
    private float dashTimerSetValue;
    private float DecreaseThisManyTimes;
    private float guiTimer;
    private float startScale = 0.2f;
    void Awake()
    {
        player = GameObject.FindWithTag("Player");  //all players should be called Character
        dashBar = GameObject.Find("DashBar");
        GuiDashtransform = dashBar.GetComponent<Transform>();
        playerScript = player.GetComponent<Player>();
        //set to 0 at start 
        GuiDashtransform.localScale = new Vector2(0f, 0f); // width and height
        dashTimerSetValue = dashTimer.GetTimerSetValue();
    }

    // Update is called once per frame
    void Update()
    {
        if (dashTimer.IsReady() == false)
        {
            //countdown
            if(fullOnce == true)
            {
                growFactor = startScale / DecreaseThisManyTimes;
                GuiDashtransform.localScale = new Vector3(GuiDashtransform.localScale.x, startScale, GuiDashtransform.localScale.z);
              //  guiTimer = 0;
                fullOnce = false;

            }
            // GuiDashtransform.localScale = new Vector2(0f, 0f);
            if (minChargeSize < GuiDashtransform.localScale.x && stopCharge == true)
            {
                stopCharge = true;
                InvokeRepeating("DecreaseBar", 0f, dashTimerSetValue / DecreaseThisManyTimes);
            }
            else
            {
                stopCharge = false;
                GuiDashtransform.localScale = new Vector3(GuiDashtransform.localScale.x, 0f, GuiDashtransform.localScale.z);

                CancelInvoke("DecreaseBar");

            }



        }
        else
        {
            fullOnce = true;
            GuiDashtransform.localScale = new Vector3(GuiDashtransform.localScale.x, 0f, GuiDashtransform.localScale.z);
        }
    }
    
    private void DecreaseBar()
    {


        //  GuiDashtransform.localScale = new Vector3(rectTransform.localScale.x, 0.62f, 0);
        GuiDashtransform.localScale -= new Vector3(0f, growFactor, 0);

    }
}
