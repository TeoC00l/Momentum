using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionToLevelChange : MonoBehaviour
{
    [SerializeField] private Transform targetLocation;
    [SerializeField] private float speedToTarget;
    private GameObject player;
    private Player playerScript;
    private PhysicsComponent physComp;
    private bool hitTrigger = false;
    private float step;
    private float finalTime;
    private UITimer timer;
    private string name;
    private Text[] NameText;
    private Text gratsText;
    private int activeText = 0;
    private GameObject nameParent;
    private GameObject leaderboard;
    private float horizontalTimer;
    private float verticalTimer;
    string st = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ";
    private List<Scores> highScores;
    private Text[] allChildren;
    private GameObject canvas;
    private GameObject hide;

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
            hitTrigger = true;
            timer.SetPlayable(false);
            finalTime = timer.GetTimeAsFloat();
            hide.SetActive(true);
            gratsText.enabled = true;
            gratsText.text = "Your Time " + timer.goodLookingTimer();
            Scene m_Scene = SceneManager.GetActiveScene();
            highScores = HighScoreManager._instance.GetHighScore(m_Scene.name);
            //  player.transform.LookAt(targetLocation);
            playerScript.SetNeutralizeInput(true);
            allChildren = leaderboard.GetComponentsInChildren<Text>();

            deployLeaderboard();


        }
    }
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
                    T.color = Color.black;

                }
                index++;
            }

            player.transform.LookAt(targetLocation);
            player.transform.position += new Vector3(player.transform.forward.x * speedToTarget, player.transform.forward.y, player.transform.forward.z * speedToTarget) ;
            physComp.SetDirection(player.transform.forward * step);
            physComp.SetVelocity(player.transform.forward * step);
            if (Vector3.Distance(player.transform.position, targetLocation.position) < 2f)
            {
                foreach (Text T in NameText)
                {
                    name += T.text;
                }
                HighScoreManager._instance.SaveHighScore(name, finalTime);
                name = "";
                SceneManager.LoadScene(2);
            }
            //    Vector3.MoveTowards(player.transform.position, targetLocation.position, step);

        }
        
    }
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
    private void deployLeaderboard()
    {
        int index = 1;
        allChildren[index - 1].text = "Current Leaderboard";

        foreach (Scores _score in highScores)
        {
            if (index == 1 && timer.GetTimeAsFloat() == _score.score)
            {
                allChildren[index].text = (index) + ": " + _score.name + " " + _score.GetTimer() + " New HighScore";

            }
            else
            {
                allChildren[index].text = (index) + ": " + _score.name + " " + _score.GetTimer();

            }
            index++;
        }

    }
}
