using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScreen : MonoBehaviour
{
    [SerializeField] private Button RestartButton, quitButton, MainMenuButton, RestartFromCheckpointButton;
    private bool ChangePause;
    // Start is called before the first frame update
    void Start()
    {
        RestartButton.onClick.AddListener(Restart);
        quitButton.onClick.AddListener(quitListener);
        MainMenuButton.onClick.AddListener(MainMenu);
        if (RestartFromCheckpointButton != null)
        {
            RestartFromCheckpointButton.onClick.AddListener(RestartFromCheckpoint);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SaveManager._instance.SetSaveBool(false);
        SetChange(true);

    }
    void quitListener()
    {
        Application.Quit();
    }
    void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    void RestartFromCheckpoint()
    {
        SaveManager._instance.TransitionToSavedCheckPoint();
        SetChange(true);
    }
    public void SetChange(bool set)
    {
        this.ChangePause = set;
    }
    public bool GetChange()
    {
        return ChangePause;
    }
    public void SelectButton(){
        RestartButton.Select();
        
    }
// Update is called once per frame
void Update()
    {
        
    }
}
