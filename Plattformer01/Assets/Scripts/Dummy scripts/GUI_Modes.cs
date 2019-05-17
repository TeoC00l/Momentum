using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Modes : MonoBehaviour
{
    private GameObject uiPrecision;
    private GameObject uiMomentum;
    private GameObject uiKinetic;

    public GameObject player;

    private bool shiftToggled = false;

    //Location for the cursor
    [SerializeField]private Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotspot = Vector2.zero;
    

    void Start()
    {
        //Get UI elements. If not Drag and drop from scene.
        uiPrecision = GameObject.Find("UIPrecision");
        uiMomentum = GameObject.Find("UIMomentum");
        uiKinetic = GameObject.Find("UIKinetic");
        player = GameObject.FindWithTag("Player"); //char names must always be Character

        //put the GUI's in default orders.
        uiMomentum.SetActive(false);
        uiKinetic.SetActive(false);

        //assign mouse
        Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
    }

    void Update()
    {
        //since it's pretty much known you can hit shift and it'll switch between each mode nontstop. this method below always works.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shiftToggled = !shiftToggled;
            if (shiftToggled) { uiPrecision.SetActive(false); uiMomentum.SetActive(true); Debug.Log("click s = 1"); }
            else { uiPrecision.SetActive(true); uiMomentum.SetActive(false); uiKinetic.SetActive(false); } //possibly remove UIKineticsetactive.
        }

        //check if kinetic battery is on or off.
        if (player.GetComponent<Player>().GetKineticActive()) { uiKinetic.SetActive(true); }
        else { uiKinetic.SetActive(false); }
    }
}
