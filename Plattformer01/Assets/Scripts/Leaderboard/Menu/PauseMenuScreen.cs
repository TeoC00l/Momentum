using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScreen : MonoBehaviour
{
    //Attributes
    [SerializeField] private Button RestartButton, quitButton, MainMenuButton, RestartFromCheckpointButton;
    private bool changePause;
    private List<Button> buttons = new List<Button>();
    private int activeButton = 0;

    //Methods
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        RestartButton.onClick.AddListener(Restart);

        quitButton.onClick.AddListener(QuitListener);
        MainMenuButton.onClick.AddListener(MainMenu);
        if (RestartFromCheckpointButton != null)
        {
            RestartFromCheckpointButton.onClick.AddListener(RestartFromCheckpoint);
        }
        RestartButton.Select();
    }

    void Awake()
    {

        buttons.Add(RestartButton);
        buttons.Add(MainMenuButton);
        buttons.Add(quitButton);
        if (RestartFromCheckpointButton != null)
        {
            buttons.Add(RestartFromCheckpointButton);
        }

    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SaveManager._instance.SetSaveBool(false);
        SetChange(true);

    }
    void QuitListener()
    {
        Time.timeScale = 1;

        Application.Quit();
    }
    void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    void RestartFromCheckpoint()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SaveManager._instance.TransitionToSavedCheckPoint();
        SetChange(true);
    }
    public void SetChange(bool set)
    {
        this.changePause = set;
    }
    public bool GetChange()
    {
        return changePause;
    }
    public void SelectButton()
    {
        Debug.Log("SELECTED");
        if (SaveManager._instance.GetSaveBool() == true)
        {
            RestartFromCheckpointButton.gameObject.SetActive(true);
            RestartFromCheckpointButton.onClick.AddListener(RestartFromCheckpoint);

        }
        else if (SaveManager._instance.GetSaveBool() == false)
        {
            RestartFromCheckpointButton.onClick.RemoveListener(RestartFromCheckpoint);
            RestartFromCheckpointButton.gameObject.SetActive(false);

        }

        RestartButton.Select();
        activeButton = 0;

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            activeButton--;
            if (activeButton < 0)
            {
                activeButton = (buttons.Count - 1);


            }
            buttons[activeButton].Select();

        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            activeButton++;
            if (activeButton > (buttons.Count - 1))
            {
                activeButton = 0;

            }
            buttons[activeButton].Select();

        }

    }
}
