using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI2_1 : MonoBehaviour
{

    //Main Author: Yukun
    private GameObject UIPrecision;
    private GameObject UIMomentum;
    private GameObject UIKinetic;

    private Player player;

    //Location for the cursor
    public Texture2D cursorTexture; //cursor is needed?
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotspot = Vector2.zero;

    public GameObject boardThruster;
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject hip;


    ////annoying part
    [Header("On Model")]
    public GameObject[] allboosters_on;

    [Header("Off Model")]
    public GameObject[] allboosters_off;


    [Header("particles")]
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
        if (player.IsPrecisionStateActive() == true)
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
            if(boardThruster.transform.eulerAngles.z < 90 )
            {
                Debug.Log("Shift");
                boardThruster.transform.Rotate(0,0,900* rotationSpeed * Time.smoothDeltaTime, Space.Self);
            }
            else if(boardThruster.transform.eulerAngles.z > 90)
            {
                boardThruster.transform.Rotate(0, 0, -900 * rotationSpeed * Time.smoothDeltaTime, Space.Self);

                Debug.Log("DontShift");

            }
            particle1.SetActive(false);
            particle2.SetActive(true);

        }
        else if (player.IsMomentumStateActive() == true)
        {

            //activate below script if you want the sound to stop play as soon as momentum is on
            //if (audioPrecision.isPlaying) { audioPrecision.Stop(); }

            precisionOn = false;
            UIMomentum.SetActive(true);
            UIPrecision.SetActive(false);
            UIKinetic.SetActive(false);
            turnOnBooster();

            if (boardThruster.transform.eulerAngles.z < 181 )
            {
                boardThruster.transform.transform.Rotate(0, 0, 180 * Time.smoothDeltaTime);
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
            boardThruster.transform.Rotate(0, 0, 360 *Time.smoothDeltaTime * 3.5f);
             
            particle1.SetActive(false);
            particle2.SetActive(false);

            audioSecondary.clip = kineticCharge;
            audioSecondary.Play();
        }

        //try to fix code which plays the sound when game starts
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
        turnOnBooster();
    }


    ////handle engine on or off section
    void turnOffBooster()
    {
        //disable all on boosters
        for (int i = 0; i < allboosters_on.Length; i++)
        {
            allboosters_on[i].GetComponent<MeshRenderer>().enabled = false;

        }

        //enable all off boosters
        for (int b = 0; b < allboosters_off.Length; b++)
        {
            allboosters_off[b].GetComponent<MeshRenderer>().enabled = true;
        }
    }


    void turnOnBooster()
    {

            //enable all on boosters
            for (int i = 0; i < allboosters_on.Length; i++)
            {
                allboosters_on[i].GetComponent<MeshRenderer>().enabled = true;
            }

            //disable all off boosters
            for (int b = 0; b < allboosters_off.Length; b++)
            {
                allboosters_off[b].GetComponent<MeshRenderer>().enabled = false;
            }
        


        
    }
}
