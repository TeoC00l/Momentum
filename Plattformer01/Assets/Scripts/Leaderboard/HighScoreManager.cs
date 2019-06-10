﻿ using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

public class HighScoreManager : MonoBehaviour
{
    private Scene m_Scene;
    private string sceneName2;
    private static HighScoreManager m_instance;
    private const int LeaderboardLength = 10;

    public static HighScoreManager _instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new GameObject("HighScoreManager").AddComponent<HighScoreManager>();
            }
            return m_instance;
        }
    }

    void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else if (m_instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void SaveHighScore(string name, float score)
    {
        m_Scene = SceneManager.GetActiveScene();
        sceneName2 = m_Scene.name;
        List<Scores> HighScores = GetHighScore(sceneName2);


        int i = 1;
        while (i <= LeaderboardLength && PlayerPrefs.HasKey(sceneName2 + i + "score"))
        {
            Scores temp = new Scores();
            temp.score = PlayerPrefs.GetFloat(sceneName2 + i + "score");
            temp.name = PlayerPrefs.GetString(sceneName2 + i + "name");
            HighScores.Add(temp);
            i++;
        }
        if (HighScores.Count == 0)
        {
            Scores _temp = new Scores();
            _temp.name = name;
            _temp.score = score;
            HighScores.Add(_temp);
        }
        else
        {
            for (i = 1; i <= HighScores.Count && i <= LeaderboardLength; i++)
            {
                if (score < HighScores[i - 1].score)
                {
                    Scores _temp = new Scores();
                    _temp.name = name;
                    _temp.score = score;
                    HighScores.Insert(i - 1, _temp);
                    break;
                }
                if (i == HighScores.Count && i < LeaderboardLength)
                {
                    Scores _temp = new Scores();
                    _temp.name = name;
                    _temp.score = score;
                    HighScores.Add(_temp);
                    break;
                }
            }
        }

        i = 1;
        while (i <= LeaderboardLength && i <= HighScores.Count)
        {
            PlayerPrefs.SetString(sceneName2 + i + "name", HighScores[i - 1].name);
            PlayerPrefs.SetFloat(sceneName2 + i + "score", HighScores[i - 1].score);
            i++;
        }

    }

    public List<Scores> GetHighScore(string sceneName)
    {
       
        List<Scores> HighScores = new List<Scores>();

        int i = 1;
        while (i <= LeaderboardLength && PlayerPrefs.HasKey(sceneName + i + "score"))
        {
            
            
            Scores temp = new Scores();
            temp.score = PlayerPrefs.GetFloat(sceneName + i + "score");
            temp.name = PlayerPrefs.GetString(sceneName + i + "name");


            HighScores.Add(temp);
            i++;
        }
        HighScores = insertionSort(HighScores, HighScores.Count);
        HighScores = RemoveDuplicates(HighScores);
        return HighScores;
    }
    private List<Scores> insertionSort( List<Scores> arr, int length)
    {
        int i, j;
        Scores key;
        for (i = 1; i < length; i++)
        {
            j = i;
            while (j > 0 && arr[j - 1].score > arr[j].score)
            {
                key = arr[j];
                arr[j] = arr[j - 1];
                arr[j - 1] = key;
                j--;
            }
        }
        return arr;
    }


    public void ClearLeaderBoard(string sceneName)
    {
        //for(int i=0;i<HighScores.
        List<Scores> HighScores = GetHighScore(sceneName);

        for (int i = 1; i <= HighScores.Count; i++)
        {
            PlayerPrefs.DeleteKey(sceneName + i + "name");
            PlayerPrefs.DeleteKey(sceneName + i + "score");
        }
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
    public List<Scores> RemoveDuplicates(List<Scores> listPoints)
    {
        List<Scores> result = new List<Scores>();

        for (int i = 0; i < listPoints.Count; i++)
        {
            bool contains = false;
            foreach (Scores _score in result)
            {
                if (listPoints[i].name == _score.name)
                {
                    contains = true;
                }
            }
            if (contains == false)
            {
                result.Add(listPoints[i]);
            }
        }

        return result;
    }
}

public class Scores
{
    public float score;
    public string name;
    public string GetTimer()
    {
        float minutes = Mathf.Floor(this.score);
        float seconds = Mathf.Floor((this.score - minutes) * 100);
        float miliseconds = Mathf.Round((((this.score - minutes) * 100) - seconds) * 100);
        return "" + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + miliseconds.ToString("00");
    }
}
