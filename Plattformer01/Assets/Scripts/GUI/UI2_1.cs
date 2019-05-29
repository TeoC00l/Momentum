using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI2_1 : MonoBehaviour
{
    private GameObject UIPrecision;
    private GameObject UIMomentum;
    private GameObject UIKinetic;

    private Player player;

    //Location for the cursor
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotspot = Vector2.zero;

    public GameObject boardThruster;

    ////annoying part
    public GameObject booster_on1;
    public GameObject booster_on2;
    public GameObject booster_off1;
    public GameObject booster_off2;
    public GameObject particle1;
    public GameObject particle2;


    public float ninetyAngle = 90f;
    public float rotationSpeed = 2f;


    void Start()
    {
        //Get UI elements. If not Drag and drop from scene.
        UIPrecision = GameObject.Find("Precision_Mode");
        UIMomentum = GameObject.Find("Momentum_Mode");
        UIKinetic = GameObject.Find("KineticText");
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        //put the GUI's in default orders.
        UIMomentum.SetActive(false);
        UIKinetic.SetActive(false);

        //assign mouse
        Cursor.SetCursor(cursorTexture, hotspot, cursorMode);

        //initialize engines
        initializeEngines();

        particle1 = GameObject.Find("BoardParticleBoost_off");
        particle2 = GameObject.Find("BoardParticleBoost_on");
    }

    void Update()
    {
        if (player.GetPrecisionActive() == true)
        {
            UIMomentum.SetActive(false);
            UIPrecision.SetActive(true);
            UIKinetic.SetActive(false);
            turnOnBooster();
            shiftNormal();
            if(boardThruster.GetComponent<Transform>().transform.eulerAngles.z < 90)
            {
                boardThruster.GetComponent<Transform>().transform.Rotate(0,0,90* rotationSpeed, Space.Self);
            }
            particle1.SetActive(false);
            particle2.SetActive(true);
        }
        else if (player.GetMomentumActive() == true)
        {
            UIMomentum.SetActive(true);
            UIPrecision.SetActive(false);
            UIKinetic.SetActive(false);
            turnOnBooster();

            //@Mugai
            //kan du fixa så att brädans rotation lerp slerp
            //typ rotate(0,0,angle_now +90)
            if (boardThruster.GetComponent<Transform>().transform.eulerAngles.z < 181 )
            {
                boardThruster.GetComponent<Transform>().transform.Rotate(0, 0, 180);
            }
            particle1.SetActive(true);
            particle2.SetActive(false);

            boardThruster.GetComponent<Transform>().eulerAngles = new Vector3(boardThruster.GetComponent<Transform>().transform.eulerAngles.x, boardThruster.GetComponent<Transform>().transform.eulerAngles.y, 0);
        }
        else if (player.GetKineticActive() == true)
        {
            UIMomentum.SetActive(true);
            UIPrecision.SetActive(false);
            UIKinetic.SetActive(true);
            turnOffBooster();
            boardThruster.GetComponent<Transform>().transform.Rotate(0, 0, 180*Time.deltaTime*3.5f);
            particle1.SetActive(false);
            particle2.SetActive(false);
        }
    }



    ////engine Functions
    void shiftNinety()
    {
        //boardThruster.GetComponent<Transform>().Rotate(0, 0, 90, Space.Self);
    }
    void shiftNormal()
    {
        boardThruster.GetComponent<Transform>().Rotate(0, 0, 0, Space.Self);
    }








    //engine Initializer
    void initializeEngines()
    {
        //booster on off. använd lerpfärg här efter kanske om vi har tid.
        booster_on1.GetComponent<MeshRenderer>().enabled = true;
        booster_off1.GetComponent<MeshRenderer>().enabled = false;
        booster_on2.GetComponent<MeshRenderer>().enabled = true;
        booster_off2.GetComponent<MeshRenderer>().enabled = false;
    }

    ////handle engine on or off section
    void turnOffBooster()
    {
        booster_on1.GetComponent<MeshRenderer>().enabled = false;
        booster_off1.GetComponent<MeshRenderer>().enabled = true;
        booster_on2.GetComponent<MeshRenderer>().enabled = false;
        booster_off2.GetComponent<MeshRenderer>().enabled = true;
    }

    void turnOnBooster()
    {
       booster_on1.GetComponent<MeshRenderer>().enabled = true;
        booster_off1.GetComponent<MeshRenderer>().enabled = false;
        booster_on2.GetComponent<MeshRenderer>().enabled = true;
        booster_off2.GetComponent<MeshRenderer>().enabled = false;
    }
}
