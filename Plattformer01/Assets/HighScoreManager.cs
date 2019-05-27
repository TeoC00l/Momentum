 using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;
using UnityEngine.SceneManagement;

 /// <summary>
 /// High score manager.
 /// Local highScore manager for LeaderboardLength number of entries
 /// 
 /// this is a singleton class.  to access these functions, use HighScoreManager._instance object.
 /// eg: HighScoreManager._instance.SaveHighScore("meh",1232);
 /// No need to attach this to any game object, thought it would create errors attaching.
 /// </summary>

public class HighScoreManager : MonoBehaviour
{
    private Scene m_Scene;
    private string sceneName;
    private static HighScoreManager m_instance;
    private const int LeaderboardLength = 10;
    Dictionary<string, List<Scores>> HighScoreDictionary = new Dictionary<string, List<Scores>>();

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
        sceneName = m_Scene.name;
        List<Scores> HighScores = GetHighScore(sceneName);


        int i = 1;
        while (i <= LeaderboardLength && PlayerPrefs.HasKey(sceneName + i + "score"))
        {
            Scores temp = new Scores();
            temp.score = PlayerPrefs.GetFloat(sceneName + i + "score");
            temp.name = PlayerPrefs.GetString(sceneName + i + "name");
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
      //  HighScoreDictionary.Add(sceneName, HighScores);

        i = 1;
        while (i <= LeaderboardLength && i <= HighScores.Count)
        {
            PlayerPrefs.SetString(sceneName + i + "name", HighScores[i - 1].name);
            PlayerPrefs.SetFloat(sceneName + i + "score", HighScores[i - 1].score);
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

        return HighScores;
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
}

public class Scores
{
    public float score;
    public string name;

}
