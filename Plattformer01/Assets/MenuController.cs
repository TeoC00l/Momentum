using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Scene m_Scene;
    [SerializeField] private Scene m_Scene2;
    [SerializeField] private Scene m_Scene3;

    private string sceneName;
    string name = "";
    string score = "";
    List<Scores> highscore;
    [SerializeField]private Texture2D buttonImageAddScore;
    [SerializeField] private Font sciFiFont;

    string text = "Add Score";
    string text2 = "Get Leaderboard";
    string text3 = "Clear Leaderboard";

    GUIContent content = new GUIContent();
    GUIContent content2 = new GUIContent();

    GUIContent content3 = new GUIContent();
    GUIStyle style = new GUIStyle();

    [SerializeField] private float ButtonsWidth;
    [SerializeField] private float ButtonsHeight;
    [SerializeField] private float ButtonsXposition;

    string[] toolbarStrings;
    int toolbarInt = 0;



    // Use this for initialization
    void Start()
    {
        content.text = text;
        content2.text = text2;
        content3.text = text3;
        highscore = new List<Scores>();
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        highscore = HighScoreManager._instance.GetHighScore(sceneName);
        style.normal.textColor = Color.cyan;

        toolbarStrings[0] = m_Scene.name;
        toolbarStrings[1] = m_Scene2.name;
        toolbarStrings[2] = m_Scene3.name;





    }


    void ButtonClicked(GameObject _obj)
    {
        print("Clicked button:" + _obj.name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {

        toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings);

        GUI.skin.button.normal.background = (Texture2D)buttonImageAddScore;

        GUI.skin.font = sciFiFont;
        GUILayout.BeginHorizontal();
        GUILayout.Label("Name :", style);
        name = GUILayout.TextField(name);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Time :", style);
        score = GUILayout.TextField(score);
        GUILayout.EndHorizontal();
        //  GUI.skin.button = buttonImageAddScore;
        sceneName = toolbarStrings[toolbarInt];
        if (GUI.Button(new Rect(ButtonsXposition, 170, ButtonsWidth, ButtonsHeight), content))
        {
            float tempScore = float.Parse(score, CultureInfo.InvariantCulture.NumberFormat);

            HighScoreManager._instance.SaveHighScore(name, tempScore);
            highscore = HighScoreManager._instance.GetHighScore(sceneName);
        }

        if (GUI.Button(new Rect(ButtonsXposition, 240, ButtonsWidth, ButtonsHeight), content2))
        {
            highscore = HighScoreManager._instance.GetHighScore(sceneName);
        }

        if (GUI.Button(new Rect(ButtonsXposition, 310, ButtonsWidth, ButtonsHeight), content3))
        {
            HighScoreManager._instance.ClearLeaderBoard(sceneName);
            highscore = HighScoreManager._instance.GetHighScore(sceneName);
        }

        GUILayout.Space(60);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Name", style, GUILayout.Width(Screen.width / 2));
        GUILayout.Label("Time", style, GUILayout.Width(Screen.width / 2));
        GUILayout.EndHorizontal();

        GUILayout.Space(25);

        foreach (Scores _score in highscore)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(_score.name, style, GUILayout.Width(Screen.width / 2));
            
            float minutes = Mathf.Floor(_score.score);
            
            float seconds = Mathf.Floor((_score.score - minutes) * 100);
         
           
            float miliseconds = Mathf.Round((((_score.score - minutes )  * 100)- seconds) * 100);
            // float miliseconds = tempmiliseconds * 10000;
            Debug.Log(miliseconds);

            GUILayout.Label("" + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + miliseconds.ToString("00"), style, GUILayout.Width(Screen.width / 2));


            GUILayout.EndHorizontal();
        }
    }
   

}

