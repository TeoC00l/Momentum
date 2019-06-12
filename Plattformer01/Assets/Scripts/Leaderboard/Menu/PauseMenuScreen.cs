using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScreen : MonoBehaviour
{
    //Attributes
    [SerializeField] private Button RestartButton, quitButton, MainMenuButton, RestartFromCheckpointButton;
    private bool ChangePause;
    private List<Button> Buttons = new List<Button>();
    private int activeButton = 0;

    //Methods
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        RestartButton.onClick.AddListener(Restart);

        quitButton.onClick.AddListener(quitListener);
        MainMenuButton.onClick.AddListener(MainMenu);
        if (RestartFromCheckpointButton != null)
        {
            RestartFromCheckpointButton.onClick.AddListener(RestartFromCheckpoint);
        }
        RestartButton.Select();
    }

    void Awake()
    {

        Buttons.Add(RestartButton);
        Buttons.Add(MainMenuButton);
        Buttons.Add(quitButton);
        if (RestartFromCheckpointButton != null)
        {
            Buttons.Add(RestartFromCheckpointButton);
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
    public void SelectButton()
    {
        Debug.Log("SELECTED");
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
                activeButton = (Buttons.Count - 1);

            }
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            activeButton++;
            if (activeButton > (Buttons.Count - 1))
            {
                activeButton = 0;
            }
        }

        Buttons[activeButton].Select();


    }
}
