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
    [SerializeField] private GameObject canvas2;

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
                Debug.Log("select button");

                canvas.GetComponent<PauseMenuScreen>().SelectButton();

            }

        }

    }
}
