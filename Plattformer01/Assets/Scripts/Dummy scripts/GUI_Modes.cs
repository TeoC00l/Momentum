﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Modes : MonoBehaviour
{
    private GameObject UIPrecision;
    private GameObject UIMomentum;
    private GameObject UIKinetic;

    private Player player;

    //Location for the cursor
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotspot = Vector2.zero;

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject canvas2;


    //public GameObject boardThruster;


    ////annoying part
    //public GameObject booster_on1;
    //public GameObject booster_on2;
    //public GameObject booster_off1;
    //public GameObject booster_off2;


    //public float ninetyAngle = 90f;
    //public float rotationSpeed = 2f;


    void Start()
    {
        //Get UI elements. If not Drag and drop from scene.
        UIPrecision = GameObject.Find("UIPrecision");
        UIMomentum = GameObject.Find("UIMomentum");
        UIKinetic = GameObject.Find("UIKinetic");
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        //put the GUI's in default orders.
        UIMomentum.SetActive(false);
        UIKinetic.SetActive(false);

        //assign mouse
       //Cursor.SetCursor(cursorTexture, hotspot, cursorMode);

        //initialize engines
        //initializeEngines();

    }

    void Update()
    {
        if (player.GetPrecisionActive() == true)
        {
            UIMomentum.SetActive(false);
            UIPrecision.SetActive(true);
            UIKinetic.SetActive(false);
            //turnOnBooster();
            //shiftNormal();
            //if(boardThruster.GetComponent<Transform>().transform.eulerAngles.z < 90)
            //{
            //    boardThruster.GetComponent<Transform>().transform.Rotate(0,0,90* rotationSpeed);
            //}
        }
        else if (player.GetMomentumActive() == true)
        {
            UIMomentum.SetActive(true);
            UIPrecision.SetActive(false);
            UIKinetic.SetActive(false);
            //turnOnBooster();

            //@Mugai
            //kan du fixa så att brädans rotation återställer till 0.
            //boardThruster.GetComponent<Transform>().transform.eulerAngles.z = 0;
            //if (boardThruster.GetComponent<Transform>().transform.eulerAngles.z < 90 )
            //{
            //    boardThruster.GetComponent<Transform>().transform.Rotate(0, 0, 0);
            //}
        }
        else if (player.GetKineticActive() == true)
        {
            UIMomentum.SetActive(true);
            UIPrecision.SetActive(false);
            UIKinetic.SetActive(true);
            //turnOffBooster();
        }
        if (Input.GetKeyDown(KeyCode.Escape) == true || canvas.GetComponent<PauseMenuScreen>().GetChange() == true || canvas2.GetComponent<PauseMenuScreen>().GetChange() == true || Input.GetKeyDown("joystick button 7"))
        {
            Debug.Log("Pause");
            if (Time.timeScale == 1)
            {

                SetCanvas(true);
                canvas.GetComponent<PauseMenuScreen>().SelectButton();
                Time.timeScale = 0;
                player.Transition<PauseState>();

            }
            else
            {
                player.TransitionBack();
                Time.timeScale = 1;
                SetCanvas(false);




            }
            canvas.GetComponent<PauseMenuScreen>().SetChange(false);
            canvas2.GetComponent<PauseMenuScreen>().SetChange(false);

        }
    }
    public void SetCanvas(bool set)
    {
        if(SaveManager._instance.GetSaveBool() == true)
        {
            canvas2.SetActive(set);

            if (set == true)
            {
                Debug.Log("select button");
                canvas2.GetComponent<PauseMenuScreen>().SelectButton();


            }

        }
        else
        {
            canvas.SetActive(set);

            if (set == true)
            {
                canvas.GetComponent<PauseMenuScreen>().SelectButton();

            }

        }

    }



    ////engine Functions
    //void shiftNinety()
    //{
    //    //boardThruster.GetComponent<Transform>().Rotate(0, 0, 90, Space.Self);
    //}
    //void shiftNormal()
    //{
    //    boardThruster.GetComponent<Transform>().Rotate(0, 0, 0, Space.Self);
    //}








    //engine Initializer
    //void initializeEngines()
    //{

    //    //boardThruster = GameObject.Find("Engine"); //find engine
    //    ////finds enginestate.
    //    //booster_on1 = GameObject.Find("boosters_on_flare1");
    //    //booster_off1 = GameObject.Find("boosters_off_flare1");
    //    //booster_on2 = GameObject.Find("boosters_on_flare2");
    //    //booster_off2 = GameObject.Find("boosters_off_flare2");

    //    //booster on off. använd lerpfärg här efter kanske om vi har tid.
    //    booster_on1.GetComponent<MeshRenderer>().enabled = true;
    //    booster_off1.GetComponent<MeshRenderer>().enabled = false;
    //    booster_on2.GetComponent<MeshRenderer>().enabled = true;
    //    booster_off2.GetComponent<MeshRenderer>().enabled = false;
    //}

    ////handle engine on or off section
    //void turnOffBooster()
    //{
    //    booster_on1.GetComponent<MeshRenderer>().enabled = false;
    //    booster_off1.GetComponent<MeshRenderer>().enabled = true;
    //    booster_on2.GetComponent<MeshRenderer>().enabled = false;
    //    booster_off2.GetComponent<MeshRenderer>().enabled = true;
    //}

    //void turnOnBooster()
    //{
    //    booster_on1.GetComponent<MeshRenderer>().enabled = true;
    //    booster_off1.GetComponent<MeshRenderer>().enabled = false;
    //    booster_on2.GetComponent<MeshRenderer>().enabled = true;
    //    booster_off2.GetComponent<MeshRenderer>().enabled = false;
    //}
}
