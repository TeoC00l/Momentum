using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Checkpoint : MonoBehaviour
{
    //Attributes
    [SerializeField] private float playerYAxisRotation;

    private Health playerHealth;
    private GameObject player;
    private CameraScript mainCamera;
    private GameObject textCanvas;
    private Text checkpointText;
    private bool canFade;
    private Color alphaColor;
    private Color originalColor;
    private float timeToFade = 1.0f;
    //Methods
    void Start()
    {
        textCanvas = GameObject.FindGameObjectWithTag("TextCanvas");
        checkpointText = textCanvas.transform.GetChild(5).GetComponent<Text>();
        canFade = true;
        alphaColor = checkpointText.color;
        originalColor = checkpointText.color;
        alphaColor.a = 0;
        checkpointText.color = Color.clear;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        mainCamera = UnityEngine.Camera.main.GetComponent<CameraScript>();
    }
    private void Update()
    {
        if (canFade)
        {
            checkpointText.color = Color.Lerp(checkpointText.color, alphaColor, timeToFade * Time.deltaTime);
            if(checkpointText.color.a == 0)
            {
                canFade = false;
            }
        }
    }

    public void SetPlayerPositionHere()
    {
        player.transform.position = gameObject.transform.position;
        mainCamera.SetYAxisRotation(playerYAxisRotation);
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player") && playerHealth.GetCheckPoint().gameObject != this.gameObject)
        {
            checkpointText.color = originalColor;
            canFade = true;
            Destroy(playerHealth.GetCheckPoint().gameObject);
            Destroy(gameObject.GetComponent<BoxCollider>());
            playerHealth.SetCheckPoint(this);
            SaveManager._instance.SaveGame();

            Debug.Log("checkpoint reached: " + this.name);
        }
    }
}
