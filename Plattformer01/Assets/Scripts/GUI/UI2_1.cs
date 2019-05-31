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
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject hip;


    ////annoying part
    public GameObject booster_on1;
    public GameObject booster_on2;
    public GameObject booster_off1;
    public GameObject booster_off2;
    public GameObject particle1;
    public GameObject particle2;


    public float ninetyAngle = 90f;
    public float rotationSpeed = 2f;



    /// <summary>
    /// Audio
    /// </summary>
    public AudioSource[] sources;
    public AudioSource audioLow;
    public AudioSource audioSecondary;
    public AudioSource audioPrecision;
    public AudioSource playerBoardSound;
    public AudioClip kineticBat;
    public AudioClip kineticCharge;
    public AudioClip precisionSound;

    public GameObject playerBoard;
    private bool keepShifting = true;


    public bool precisionOn = false;
    
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


        //audio
        sources = GetComponents<AudioSource>();
        audioLow = sources[0];
        audioSecondary = sources[1];
        audioPrecision = sources[2];

        playerBoard = GameObject.Find("Hoverboard Sound Effects");
        playerBoardSound = playerBoard.GetComponent<AudioSource>();

    }

    void Update()
    {
        if (player.GetPrecisionActive() == true)
        {

            precisionOn = true;
            //plays the precision sound 
            if (!player.GetKineticActive() && precisionOn && !audioPrecision.isPlaying)
            {
                audioPrecision.clip = precisionSound;
                audioPrecision.Play();
            }

            audioLow.clip = precisionSound;
            audioLow.Play();
            UIMomentum.SetActive(false);
            UIPrecision.SetActive(true);
            UIKinetic.SetActive(false);
            turnOnBooster();
            if(boardThruster.transform.eulerAngles.z < 90 && keepShifting == true)
            {
                Debug.Log("Shift");
                boardThruster.transform.Rotate(0,0,90* rotationSpeed, Space.Self);
                keepShifting = false;
            }
            else
            {
                Debug.Log("DontShift");

            }
            particle1.SetActive(false);
            particle2.SetActive(true);

        }
        else if (player.GetMomentumActive() == true)
        {

            //activate below script if you want the sound to stop play as soon as momentum is on
            //if (audioPrecision.isPlaying) { audioPrecision.Stop(); }

            precisionOn = false;
            UIMomentum.SetActive(true);
            UIPrecision.SetActive(false);
            UIKinetic.SetActive(false);
            turnOnBooster();

            //@Mugai
            //kan du fixa så att brädans rotation lerp slerp
            //typ rotate(0,0,angle_now +90)
            if (boardThruster.transform.eulerAngles.z < 181 )
            {
                boardThruster.transform.transform.Rotate(0, 0, 180);
            }
            particle1.SetActive(true);
            particle2.SetActive(false);

            boardThruster.transform.eulerAngles = new Vector3(boardThruster.GetComponent<Transform>().transform.eulerAngles.x, boardThruster.GetComponent<Transform>().transform.eulerAngles.y, 0);
        }
        else if (player.GetKineticActive() == true)
        {
            playerBoardSound.volume = 0;
            precisionOn = false;
            UIMomentum.SetActive(true);
            UIPrecision.SetActive(false);
            UIKinetic.SetActive(true);
            turnOffBooster();
            boardThruster.transform.Rotate(0, 0, 180*Time.deltaTime*3.5f);
             
            particle1.SetActive(false);
            particle2.SetActive(false);

            audioSecondary.clip = kineticCharge;
            audioSecondary.Play();
        }

        //for the audio. this causes a bug which plays it at the beginning of each level. rip.
        //make startup sequence code?
        if(player.GetKineticActive() == false)
        {
          //  boardThruster.transform.rotation = hip.transform.rotation;
            playerBoardSound.volume = 0.116f;
            audioLow.clip = kineticBat;
            audioLow.Play();
        }
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
