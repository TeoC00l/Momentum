using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticCooldownGUi : MonoBehaviour
{
    private GameObject player; //player
    private GameObject dashBar; //the chargebar
    private float FullChargeSize = 0.5f; //where the battery tops off
    private float minChargeSize = 0; //where the battery stops
    private float growFactor;
    private RectTransform GuiDashtransform;
    private Player playerScript;
    private bool stopCharge = true;
    private bool fullOnce = true;
    [SerializeField] private Timer kineticTimer;
    private float kineticTimerSetValue;

    private float DecreaseThisManyTimes;
    private float guiTimer;
    private float startScale = 8f;
    void Awake()
    {
        player = GameObject.FindWithTag("Player");  //all players should be called Character
        dashBar = this.gameObject;
        GuiDashtransform = dashBar.GetComponent<RectTransform>();
        playerScript = player.GetComponent<Player>();
        //set to 0 at start 
       // width and height
        kineticTimerSetValue = kineticTimer.GetTimerSetValue();
        DecreaseThisManyTimes = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (kineticTimer.IsReady() == false && playerScript.GetKineticActive() == false)
        {
            Debug.Log("ReadyFalse");
            //countdown
            if (fullOnce == true)
            {
                growFactor = startScale / DecreaseThisManyTimes;
                GuiDashtransform.localScale = new Vector3(startScale, 1, 1);

                //  guiTimer = 0;
                fullOnce = false;

            }
            // GuiDashtransform.localScale = new Vector2(0f, 0f);
            if (minChargeSize < GuiDashtransform.localScale.y && stopCharge == true)
            {
                stopCharge = false;
                InvokeRepeating("DecreaseBar", 0f, (kineticTimerSetValue) / DecreaseThisManyTimes);
            }



        }
        else
        {
            CancelInvoke("DecreaseBar");

            stopCharge = true;
            fullOnce = true;
            GuiDashtransform.localScale = new Vector3(0f, GuiDashtransform.localScale.y, GuiDashtransform.localScale.z);
        }
    }

    private void DecreaseBar()
    {


        //  GuiDashtransform.localScale = new Vector3(rectTransform.localScale.x, 0.62f, 0);
        GuiDashtransform.localScale -= new Vector3(growFactor, 0, 0);
        Debug.Log(GuiDashtransform.localScale);

    }
}
