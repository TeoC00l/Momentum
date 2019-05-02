using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Modes : MonoBehaviour
{
    public GameObject UIPrecision;
    public GameObject UIMomentum;
    public GameObject UIKinetic;

    public GameObject player;

    private bool shiftToggled = false;
    private bool mouseToggled = true;

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
        player = GameObject.Find("Character"); //char names must always be Character

        //put the GUI's in default orders.
        UIMomentum.SetActive(false);
        UIKinetic.SetActive(false);

        //assign mouse
        Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
    }

    void Update()
    {
        //keycode below checks keyhits. to see if momentum or precision mode. the use is pretty simple and does not need to rely on player.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shiftToggled = !shiftToggled;
            if (shiftToggled) { UIPrecision.SetActive(false); UIMomentum.SetActive(true); Debug.Log("click s = 1"); }
            else { UIPrecision.SetActive(true); UIMomentum.SetActive(false); UIKinetic.SetActive(false); }

        }

        //if(player.GetComponent<Player>().getState(String));


        if (Input.GetMouseButton(0))
        {
            //if you have activated momentum mode before.
            mouseToggled = !mouseToggled;
            if (shiftToggled && mouseToggled) { UIKinetic.SetActive(true); }
            else if (shiftToggled && !mouseToggled) { UIKinetic.SetActive(false); }
            else { UIKinetic.SetActive(false); }
        }
    }
}
