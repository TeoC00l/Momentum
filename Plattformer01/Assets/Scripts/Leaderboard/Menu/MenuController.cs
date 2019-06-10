using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
     private Scene m_Scene;

    private string sceneName;
    string name = "";
    string score = "";
    List<Scores> highscore;
    [SerializeField]private Texture2D buttonImageAddScore;
    [SerializeField] private Font sciFiFont;

    string text = "Add Score";
    string text2 = "Return";
    string text3 = "Clear Leaderboard";

    GUIContent content = new GUIContent();
    GUIContent content2 = new GUIContent();

    GUIContent content3 = new GUIContent();
    GUIStyle style = new GUIStyle();

    [SerializeField] private float ButtonsWidth;
    [SerializeField] private float ButtonsHeight;
    [SerializeField] private float ButtonsXposition;


    string[] toolbarStrings;
    List<string> toolbarListString;
    int selGridInt = 0;
    int sceneCount;




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
        sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        string[] tempToolbarStrings = new string[sceneCount];
        Debug.Log("this is scene"+sceneCount);
        int index = 0;
        for (int i = 0; i < sceneCount; i++)
        {
            string nameOfScene = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
            string firstFive = nameOfScene.Substring(0, 5);
            Debug.Log(firstFive);

            if (firstFive == "Level")
            {
                tempToolbarStrings[index] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
                index++;
            }
        }
        toolbarStrings = new string[index];
        index = 0;

        foreach (string s in tempToolbarStrings)
        {
            toolbarStrings[index] = s;
            index++;
        }
        
    }

    void OnGUI()
    {
        GUILayout.BeginVertical();
        selGridInt = GUILayout.SelectionGrid(selGridInt, toolbarStrings, 1);
        GUILayout.EndVertical();

        sceneName = toolbarStrings[selGridInt];
        highscore = HighScoreManager._instance.GetHighScore(sceneName);

        GUI.skin.button.normal.background = (Texture2D)buttonImageAddScore;

        GUI.skin.font = sciFiFont;
       

       

        if (GUI.Button(new Rect(ButtonsXposition, 240, ButtonsWidth, ButtonsHeight), content2))
        {
            SceneManager.LoadScene(0);

        }

        if (GUI.Button(new Rect(ButtonsXposition, 310, ButtonsWidth, ButtonsHeight), content3))
        {
            HighScoreManager._instance.ClearLeaderBoard(sceneName);
            highscore = HighScoreManager._instance.GetHighScore(sceneName);
        }

        GUILayout.Space(60);

        GUILayout.BeginHorizontal();
        GUILayout.Label("             Name", style, GUILayout.Width(Screen.width / 2));
        GUILayout.Label("Time", style, GUILayout.Width(Screen.width / 2));
        GUILayout.EndHorizontal();

        GUILayout.Space(25);

        foreach (Scores _score in highscore)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label( "             "+ _score.name, style, GUILayout.Width(Screen.width / 2));
            
          

            GUILayout.Label(_score.GetTimer(), style, GUILayout.Width(Screen.width / 2));
            GUILayout.EndHorizontal();
        }
    }
   

}

