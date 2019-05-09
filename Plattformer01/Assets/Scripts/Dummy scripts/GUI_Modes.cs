//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GUI_Modes : MonoBehaviour
//{
//    public GameObject UIPrecision;
//    public GameObject UIMomentum;
//    public GameObject UIKinetic;

//    public GameObject player;

//    private bool shiftToggled = false;

//    //Location for the cursor
//    public Texture2D cursorTexture;
//    public CursorMode cursorMode = CursorMode.Auto;
//    public Vector2 hotspot = Vector2.zero;
    

//    void Start()
//    {
//        //Get UI elements. If not Drag and drop from scene.
//        UIPrecision = GameObject.Find("UIPrecision");
//        UIMomentum = GameObject.Find("UIMomentum");
//        UIKinetic = GameObject.Find("UIKinetic");
//        player = GameObject.Find("Character"); //char names must always be Character

//        //put the GUI's in default orders.
//        UIMomentum.SetActive(false);
//        UIKinetic.SetActive(false);

//        //assign mouse
//        Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
//    }

//    void Update()
//    {
//        //since it's pretty much known you can hit shift and it'll switch between each mode nontstop. this method below always works.
//        if (Input.GetKeyDown(KeyCode.LeftShift))
//        {
//            shiftToggled = !shiftToggled;
//            if (shiftToggled) { UIPrecision.SetActive(false); UIMomentum.SetActive(true); Debug.Log("click s = 1"); }
//            else { UIPrecision.SetActive(true); UIMomentum.SetActive(false); UIKinetic.SetActive(false); } //possibly remove UIKineticsetactive.
//        }

//        //check if kinetic battery is on or off.
//        if (player.GetComponent<Player>().GetKineticBatteryActive()) { UIKinetic.SetActive(true); }
//        else { UIKinetic.SetActive(false); }
//    }
//}
