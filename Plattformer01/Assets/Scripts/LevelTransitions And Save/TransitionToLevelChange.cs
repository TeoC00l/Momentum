using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionToLevelChange : MonoBehaviour
{
    [SerializeField] private Transform targetLocation;
    [SerializeField] private float speedToTarget;
    private bool hitTrigger = false;
    private UITimer timer;
    private string name = "aaa";
    private Text[] allChildren;
    private Text[] NameText;
    private Text gratsText;
    private int activeText = 0;
    private bool newHighscore = false;

    private GameObject nameParent;
    private GameObject leaderboard;
    private GameObject canvas;
    private GameObject hide;


    // Variables to Neutralize Player Movement
    private GameObject player;
    private Player playerScript;
    private PhysicsComponent physComp;


    private float horizontalTimer = 1;
    private float verticalTimer = 1;
    private float step;
    private float finalTime;
    private int yourScoreIndex = 11;
    private int yourPlacement;

    private string st = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private List<Scores> highScores;



    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        canvas = GameObject.FindWithTag("TextCanvas");
        playerScript = player.GetComponent<Player>();
        physComp = player.GetComponent<PhysicsComponent>();
        timer = canvas.GetComponent<UITimer>();
        nameParent = GameObject.FindWithTag("NameParent");
        leaderboard = GameObject.FindWithTag("Leaderboard");
        hide = GameObject.FindWithTag("Hide");
        gratsText = GameObject.Find("CongratsText").GetComponent<Text>();
        NameText = nameParent.GetComponentsInChildren<Text>();

        hide.SetActive(false);
        gratsText.enabled = false;


       

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SaveManager._instance.SetSaveBool(false);
            hitTrigger = true;
            timer.SetPlayable(false);
            finalTime = timer.GetTimeAsFloat();
            hide.SetActive(true);
            gratsText.enabled = true;
            gratsText.text = timer.goodLookingTimer();
            Scene m_Scene = SceneManager.GetActiveScene();
            highScores = HighScoreManager._instance.GetHighScore(m_Scene.name);
            //  player.transform.LookAt(targetLocation);
            playerScript.SetNeutralizeInput(true);
            allChildren = leaderboard.GetComponentsInChildren<Text>();

            deployLeaderboard();


        }
    }

    // Update Leaderboard at the end of the level
    void FixedUpdate()
    {
        if (hitTrigger)
        {
           

            float input = Input.GetAxisRaw("Horizontal");
            if(horizontalTimer > 0)
            {
                horizontalTimer -= Time.deltaTime;
            }
            else
            {
                horizontalTimer = 0;
            }
            if (input != null && horizontalTimer == 0)
            {
                horizontalTimer = 0.1f;
                activeText += -fixInput(input);
                if (activeText < 0)
                {
                    activeText = (NameText.Length - 1);
                }
                else if (activeText > (NameText.Length - 1))
                {
                    activeText = 0;
                }
               
            }
            if (verticalTimer > 0)
            {
                verticalTimer -= Time.deltaTime;
            }
            else
            {
                verticalTimer = 0;
            }
            int index = 0;

            foreach (Text T in NameText)
            {
                if(index == activeText)
                {
                    T.color = Color.yellow;
                    if (Input.GetAxisRaw("Vertical") != 0 && verticalTimer == 0)
                    {
                        verticalTimer = 0.1f;
                        GoThroughAlphabet(fixInput(Input.GetAxisRaw("Vertical")));
                    }
                    
                }
                else
                {
                    T.color = Color.blue;

                }
                index++;
            }

            player.transform.LookAt(targetLocation);
            player.transform.position += new Vector3(player.transform.forward.x * speedToTarget, player.transform.forward.y, player.transform.forward.z * speedToTarget) ;
            physComp.SetDirection(player.transform.forward * step);
            physComp.SetVelocity(player.transform.forward * step);
            name = "";
            foreach (Text T in NameText)
            {
                
                name += T.text;
            }
            if(newHighscore == true)
            {
                allChildren[yourScoreIndex].text = (yourPlacement) + ": " + name + " " + timer.goodLookingTimer() + " New Highscore";

            }
            else
            {
                allChildren[yourScoreIndex].text = (yourPlacement) + ": " + name + " " + timer.goodLookingTimer();

            }

            if (Vector3.Distance(player.transform.position, targetLocation.position) < 2f || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
               
                HighScoreManager._instance.SaveHighScore(name, finalTime);
                name = "";
                SceneManager.LoadScene(0);
            }
            //    Vector3.MoveTowards(player.transform.position, targetLocation.position, step);

        }
        
    }
    // Go through a-z when reaching end of game and writing in name
    private void GoThroughAlphabet(int input)
    {
        int index = 0;
        foreach (char c in st)
        {
            if(NameText[activeText].text[0] == c)
            {
                
                if(index - input < 0)
                {
                    index = (st.Length - 1);
                }else if (index - input + 1 > st.Length)
                {
                    index = 0;
                }
                else
                {
                    index = index - input;
                }
                NameText[activeText].text = "";
                NameText[activeText].text += st[index];
                break;
            }

            index++;
        }
    }
    private int fixInput (float input)
    {
        if (input > 0)
        {
           return -1;
        }
        else if (input < 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    // Get the current Score too display leaderboard
    private void deployLeaderboard()
    {
        bool scoreAdded = false;
        int index = 1;
        int Placement = 1;
        allChildren[index - 1].text = "Current Leaderboard";
        float before = 100;
        foreach (Scores _score in highScores)
        {
            if (index > 10)
            {
                break;
            }

            if (_score.score >= timer.GetTimeAsFloat())
            {
                scoreAdded = true;
                yourScoreIndex = index;
                yourPlacement = Placement;
                if (index == 1)
                {
                    newHighscore = true;

                }
                allChildren[index].text = (Placement) + ": " + name + " " + timer.goodLookingTimer();
                allChildren[yourScoreIndex].color = Color.yellow;

                index++;
                if (timer.GetTimer() != before)
                {
                    Placement++;
                }
                before = timer.GetTimeAsFloat();

            }

            allChildren[index].text = (Placement) + ": " + _score.name + " " + _score.GetTimer();

            
            index++;
            if (_score.score != before)
            {
                Placement++;
            }
            before = _score.score;
            
        }
        if (highScores.Count == 0)
        {
            yourScoreIndex = index;
            yourPlacement = Placement;
            if (index == 1)
            {
                newHighscore = true;

            }
            allChildren[index].text = (Placement) + ": " + name + " " + timer.goodLookingTimer();
            allChildren[yourScoreIndex].color = Color.yellow;
        }else if (scoreAdded == false)
        {
            yourScoreIndex = index;
            yourPlacement = Placement;
            allChildren[index].text = (Placement) + ": " + name + " " + timer.goodLookingTimer();
            allChildren[yourScoreIndex].color = Color.yellow;

        }

    }
}
