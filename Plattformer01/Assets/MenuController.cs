using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;

public class MenuController : MonoBehaviour
{

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


    private float sixthOfScreenW = 10000;
    private float sixthOfScreenH = 10000;

    // Use this for initialization
    void Start()
    {
        content.text = text;
        content2.text = text2;
        content3.text = text3;
        highscore = new List<Scores>();
        highscore = HighScoreManager._instance.GetHighScore();
        style.normal.textColor = Color.cyan;




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
      
        if (GUI.Button(new Rect(ButtonsXposition, 170, ButtonsWidth, ButtonsHeight), content))
        {
            float tempScore = float.Parse(score, CultureInfo.InvariantCulture.NumberFormat);

            HighScoreManager._instance.SaveHighScore(name, tempScore);
            highscore = HighScoreManager._instance.GetHighScore();
        }

        if (GUI.Button(new Rect(ButtonsXposition, 240, ButtonsWidth, ButtonsHeight), content2))
        {
            highscore = HighScoreManager._instance.GetHighScore();
        }

        if (GUI.Button(new Rect(ButtonsXposition, 310, ButtonsWidth, ButtonsHeight), content3))
        {
            HighScoreManager._instance.ClearLeaderBoard();
            highscore = HighScoreManager._instance.GetHighScore();
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

