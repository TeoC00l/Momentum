using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Modes : MonoBehaviour
{
    private GameObject UIPrecision;
    private GameObject UIMomentum;
    private GameObject UIKinetic;
    private Player player;

    private Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotspot = Vector2.zero;

    [SerializeField] private GameObject canvas;
    private PauseMenuScreen pauseScreen;

    void Start()
    {
        //Get UI elements. If not Drag and drop from scene.
        UIPrecision = GameObject.Find("UIPrecision");
        UIMomentum = GameObject.Find("UIMomentum");
        UIKinetic = GameObject.Find("UIKinetic");
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        pauseScreen = canvas.GetComponent<PauseMenuScreen>();
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
        else if (player.GetKineticActive() == true)
        {
            UIMomentum.SetActive(true);
            UIPrecision.SetActive(false);
            UIKinetic.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) == true  || pauseScreen.GetChange() == true || Input.GetKeyDown("joystick button 7"))
        {
            Debug.Log("Pause");
            if (Time.timeScale == 1)
            {

                canvas.SetActive(true);
                pauseScreen.SelectButton();
                player.Transition<PauseState>();

                Time.timeScale = 0;

            }
            else if(Time.timeScale == 0)
            {
                player.TransitionBack();
                Time.timeScale = 1;
                canvas.SetActive(false);
                
            }
            // canvas.GetComponent<PauseMenuScreen>().SetChange(false);
            pauseScreen.SetChange(false);

        }
    }
    
}
