using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secondary_Soundtrack : MonoBehaviour
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

    //Set Spatial Blend to 1. So that it registers to the AudioListener.

    void Start()
    {
        //plays both sounds at the same time.
        instantiateAll();
    }

    void instantiateAll()
    {
        sources = GetComponents<AudioSource>();
        basicsource2 = sources[0];
        basicsource2.volume = maxVol;
        basicsource2.PlayOneShot(music_ver1);


        basicsource3 = sources[1];
        basicsource3.volume = minVol;
        basicsource3.PlayOneShot(music_ver2);

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
    }

    void testForStateofMusic()
    {
        if (basicsource2.volume >= tempMaxVol && basicsource3.volume <= tempMinVol)
        {
            songSwitch = true;
            Debug.Log("palenq1");
        }
        else if (basicsource3.volume >= tempMaxVol && basicsource2.volume <= tempMinVol)
        {
            Debug.Log("palenq2");
            songSwitch = false;
        }
        else
        {
            Debug.Log("oops");
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
        if (!areaExited)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                if (basicsource3.volume > minVol && basicsource2.volume > minVol)
                {
                    Debug.Log("testing similarities");
                    //testForStateofMusic();
                }
                else
                {

                    MusicState();
                }

            }
        }

        if (areaExited || areaEntered)
        {
            chooseFadeStyle();
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
            Debug.Log("Track 1 playing");
        }
        else if (songSwitch)
        {
            basicsource2.volume = maxVol;
            basicsource3.volume = 0;
            songSwitch = !songSwitch;
            tempMinVol = maxVol;
            tempMaxVol = 0;
            Debug.Log("Track 2 playing");
        }
    }
}