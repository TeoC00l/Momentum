using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public Button playButton, quitButton;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(playListener);
        quitButton.onClick.AddListener(quitListener);

    }

    void playListener()
    {
        SceneManager.LoadScene(1);
    }

    void quitListener()
    {
        Application.Quit();
    }

}
