using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] private Button playButton, quitButton, playbutton2, leaderboardButton, CheckpointButton;
    [SerializeField] private GameObject checkpoint;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(playListener);
        quitButton.onClick.AddListener(quitListener);
        playbutton2.onClick.AddListener(playListener2);
        leaderboardButton.onClick.AddListener(Leaderboardtransition);
        if(SaveManager._instance.GetSaveBool() == true)
        {
            CheckpointButton.onClick.AddListener(RestartFromCheckpoint);
            checkpoint.SetActive(true);
        }
        else
        {
            checkpoint.SetActive(false);

        }
        playButton.Select();
    }

    void playListener()
    {
        SceneManager.LoadScene(1);
    }
    void playListener2()
    {
        SceneManager.LoadScene(2);

    }
    void Leaderboardtransition()
    {
        SceneManager.LoadScene(3);

    }

    void quitListener()
    {
        Application.Quit();
    }
    void RestartFromCheckpoint()
    {
        SceneManager.LoadScene(SaveManager._instance.GetScene().name);

        SaveManager._instance.TransitionToSavedCheckPoint();
    }

}
