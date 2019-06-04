using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class SaveManager : MonoBehaviour
{
    private static SaveManager m_instance;
    private Dictionary<GameObject, Vector3> Drones = new Dictionary<GameObject, Vector3>();
    private Vector3 playerPosition;
    private Quaternion playerRotation;

    private float gemSpeedIncrease;
    private Dictionary<GameObject, Vector3> Gems = new Dictionary<GameObject, Vector3>();
    private List<DroneSpawn> tempDroneSpawnList = new List<DroneSpawn>();

    private GameObject droneSpawnParent;
    private GameObject gemParent;
    private bool activeSave = false;
    private Scene m_Scene;

    private GameObject player;
    private GameObject canvas;
    private GameObject gem;
    private float savedPickUpAmount;
    private float timer;
    private GemSpeedPickUP gemPickup;



    public static SaveManager _instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new GameObject("SaveManager").AddComponent<SaveManager>();
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
    

    public void SaveGame()
    {
        Debug.Log("SaveGame");
        if(Gems != null)
        {
            Gems.Clear();

        }
        if(Drones != null)
        {
            Drones.Clear();

        }
        droneSpawnParent = GameObject.FindWithTag("DroneSpawn");

        if (tempDroneSpawnList != null)
        {
            tempDroneSpawnList.Clear();

        }
        foreach (Transform child in droneSpawnParent.transform)
        {
            DroneSpawn D = child.transform.gameObject.GetComponent<DroneSpawn>();
            tempDroneSpawnList.Add(D);
            //child is your child transform
        }
        foreach(DroneSpawn D in tempDroneSpawnList)
        {
            if(D.GetDrone() != null)
            {
                Drones.Add(D.GetDrone(), D.GetDrone().transform.position);
            }
        }
        gemParent = GameObject.FindWithTag("GemParent");
        foreach (Transform child in gemParent.transform)
        {
            if (child != null)
            {
                Gems.Add(child.gameObject, child.position);
            }
        }
        player = GameObject.FindWithTag("Player");
        playerPosition = player.transform.position;
        playerRotation = player.transform.rotation;
        m_Scene = SceneManager.GetActiveScene();
        gemSpeedIncrease = player.GetComponent<PhysicsComponent>().GetSpeedIncrease();
        canvas = GameObject.FindWithTag("TextCanvas");
        gemPickup = canvas.GetComponent<GemSpeedPickUP>();
        savedPickUpAmount = gemPickup.GetPickUpAmount();

        timer = canvas.GetComponent<UITimer>().GetTimer();

        activeSave = true;
        Debug.Log("SaveGAme"+ activeSave);


    }

    public void TransitionToSavedCheckPoint()
    {
        Debug.Log("hittransition" + activeSave);
        if(activeSave == true)
        {
            Debug.Log("hittransition2");

            if (SceneManager.GetActiveScene() != m_Scene)
            {
                SceneManager.LoadScene(m_Scene.name);

            }

            player = GameObject.FindWithTag("Player");
            player.transform.position = playerPosition;
            player.transform.rotation = playerRotation;
            player.GetComponent<PhysicsComponent>().SetSpeedIncrease(gemSpeedIncrease);
            Debug.Log(player.GetComponent<PhysicsComponent>().GetSpeedIncrease() + gemSpeedIncrease);
            canvas = GameObject.FindWithTag("TextCanvas");
            gemPickup = canvas.GetComponent<GemSpeedPickUP>();
            gemPickup.SetPickUpAmount(savedPickUpAmount);
            canvas.transform.GetChild(2).GetComponent<Text>().text = "+" + ((gemSpeedIncrease - 1) * 100) + "%";
            canvas.GetComponent<UITimer>().SetTimer(timer);
            droneSpawnParent = GameObject.FindWithTag("DroneSpawn");
            
            int index = 0;
            foreach (KeyValuePair<GameObject, Vector3> D in Drones)
            {

                droneSpawnParent.transform.GetChild(index).gameObject.GetComponent<DroneSpawn>().SpecifikSpawn(D.Value,player.transform);
                index++;
            }
            gemParent = GameObject.FindWithTag("GemParent");
            foreach (Transform child in gemParent.transform)
            {
                Destroy(child.transform.gameObject);
            }
            foreach (KeyValuePair<GameObject, Vector3> G in Gems)
            {
                if (G.Key == null)
                {
                    Debug.Log(gem);
                    GameObject theG = Instantiate(gem, G.Value, Quaternion.identity);
                    theG.transform.parent = gemParent.transform;

                }
            }



        }
    }
    public bool GetSaveBool()
    {
        return activeSave;
    }
    public void SetSaveBool(bool set)
    {
        this.activeSave = set;
    }
    public void SetGem(GameObject set)
    {
        gem = set;
    }
    public Scene GetScene()
    {
        return m_Scene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
