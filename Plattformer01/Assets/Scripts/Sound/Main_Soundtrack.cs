using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//depreciating this for now.

public class Main_Soundtrack : MonoBehaviour
{
    [Header("Basic Audio")]
    private bool active = true; //active can be called to activate or deactivate sound completely.
    private AudioSource[] sources;
    private AudioSource basicsource2;
    private AudioSource basicsource3;
    private AudioSource out_of_bounds_Music;
    public AudioClip music_ver1;
    public AudioClip music_ver2;
    private bool songSwitch = false;
    private bool areaExited = false;
    private bool areaEntered = false;
    public float fadeSpeed = 3f;
    private int FadeType = 0;
    [Header("Volumes")]
    public float minVol = 0f;
    public float maxVol = 0.14f;
    private float tempMinVol;
    private float tempMaxVol;
    private float tempS1Vol;
    private float tempS2Vol;
    private bool saveState;
    public bool isPlaying = false;

    [Header("Other Music Instances")]
    public UnityEngine.Events.UnityEvent redoInstance;

    //Set Spatial Blend to 1. So that it registers to the AudioListener.

    void Start()
    {
        //plays both sounds at the same time.
        instantiateAll();
    }
    
    public void instantiateAll()
    {
        sources = GetComponents<AudioSource>();
        basicsource2 = sources[0];
        basicsource2.volume = maxVol;
        basicsource2.Play();


        basicsource3 = sources[1];
        basicsource3.volume = minVol;
        basicsource3.Play();

        tempS1Vol = basicsource2.volume;
        tempS2Vol = basicsource3.volume;
        saveState = songSwitch;

    }

    private void OnTriggerEnter(Collider other)
    {
        songSwitch = saveState;
        //load state of volume
        basicsource2.volume = tempS1Vol;
        basicsource3.volume = tempS2Vol;
        areaEntered = true;
        FadeType = 1;
        isPlaying = true;
        basicsource2.Stop();
        basicsource3.Stop();
        basicsource2.Play();
        basicsource3.Play();
    }


    //what is happening is that when you leave trigger. It stops all sounds. so one instance flows to the next.
    public void restartSounds()
    {
        if(!basicsource2.isPlaying && !basicsource3.isPlaying)
        {
            instantiateAll();
        }
    }


    


    private void OnTriggerExit(Collider other)
    {
        //save state of vol
        tempS1Vol = basicsource2.volume;
        tempS2Vol = basicsource3.volume;

        areaExited = true;
        FadeType = 2;
        saveState = songSwitch;
        basicsource2.Stop();
        basicsource3.Stop();
    }

    // Update is called once per frame
    void Update()
    {

            if (Input.GetKeyDown(KeyCode.B))
            {
                MusicState();
            }
        

        if(areaExited || areaEntered)
        {
            chooseFadeStyle();
        }

        if(basicsource2.volume > 0 && basicsource3.volume > 0)
        {
           
            basicsource2.Stop();
            basicsource3.Stop();
            instantiateAll();
            songSwitch = false;
        }

        if(basicsource2.isPlaying || basicsource3.isPlaying)
        {
            isPlaying = true;
        }
    }




    void chooseFadeStyle()
    {
        if (FadeType == 1)
        {
            FadeIn();
        }
        if (FadeType == 2)
        {
            FadeOut();
        }
    }

    void FadeIn()
    {
        if (basicsource2.volume < maxVol)
        {
            basicsource2.volume = basicsource2.volume + (Time.deltaTime / fadeSpeed);
        }
        else if (basicsource2.volume >= maxVol)
        {
            FadeType = 0;
        }
        else if (basicsource3.volume < minVol)
        {
            basicsource3.volume = basicsource3.volume + (Time.deltaTime / fadeSpeed);
        }
    }

    void FadeOut()
    {
        if (basicsource2.volume < 1)
        {
            basicsource2.volume = basicsource2.volume - (Time.deltaTime / (fadeSpeed + 1));
        }
        else if (basicsource2.volume >= maxVol)
        {
            FadeType = 0;
        }
        else if (basicsource3.volume < 1)
        {
            basicsource3.volume = basicsource3.volume - (Time.deltaTime / (fadeSpeed + 1));
        }
        
    }

    //MusicState() is good for saving the current state of the music!!
    void MusicState()
    {
        if (!songSwitch)
        {

            basicsource2.volume = 0;
            basicsource3.volume = maxVol;
            songSwitch = !songSwitch;
            tempMinVol = 0;
            tempMaxVol = maxVol;
           
        }
        else if (songSwitch)
        {
            basicsource2.volume = maxVol;
            basicsource3.volume = 0;
            songSwitch = !songSwitch;
            tempMinVol = maxVol;
            tempMaxVol = 0;
            
        }
    }
}
