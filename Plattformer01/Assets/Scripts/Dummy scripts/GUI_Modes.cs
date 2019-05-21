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
    

    void Start()
    {
        //Get UI elements. If not Drag and drop from scene.
        UIPrecision = GameObject.Find("UIPrecision");
        UIMomentum = GameObject.Find("UIMomentum");
        UIKinetic = GameObject.Find("UIKinetic");
        player = GameObject.Find("Character").GetComponent<Player>();

        //put the GUI's in default orders.
        UIMomentum.SetActive(false);
        UIKinetic.SetActive(false);

        //assign mouse
        Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
    }

    void Update()
    {
        if (player.GetPrecisionActive() == true)
        {
            UIMomentum.SetActive(false);
            UIPrecision.SetActive(true);
            UIKinetic.SetActive(false);
        }
        else if (player.GetMomentumActive() == true)
        {
            UIMomentum.SetActive(true);
            UIPrecision.SetActive(false);
            UIKinetic.SetActive(false);
        }

        if (player.GetKineticBatteryActive() == true)
        {
            Debug.Log("Kinetic ACtive");
            UIMomentum.SetActive(false);
            UIPrecision.SetActive(false);
            UIKinetic.SetActive(true);
        }

    }
}
