using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitToMenu : MonoBehaviour
{
    public Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        quitButton.onClick.AddListener(quitListener);
    }

    // Update is called once per frame
    void quitListener()
    {
        SceneManager.LoadScene(0);
    }
}
