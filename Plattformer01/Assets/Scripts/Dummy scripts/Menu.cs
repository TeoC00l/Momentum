using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] private Button playButton, quitButton, playbutton2, leaderboardButton;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(playListener);
        quitButton.onClick.AddListener(quitListener);
        playbutton2.onClick.AddListener(playListener2);
        leaderboardButton.onClick.AddListener(Leaderboardtransition);
      
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
  

}
