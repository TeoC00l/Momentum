using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicPlay : MonoBehaviour
{
    public float bpm = 140.0f;
    public int numBeatsPerSegment = 8;
    public AudioClip[] clips = new AudioClip[2];

    private double nextEventTime;
    private int flip = 0;
    private AudioSource[] audioSources = new AudioSource[2];
    private bool running = false;

    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject child = new GameObject("SoundTrack");
            child.transform.parent = gameObject.transform;
            audioSources[i] = child.AddComponent<AudioSource>();
        }

        nextEventTime = AudioSettings.dspTime + 2.0f;
        running = true;
    }

    void Update()
    {
        if (!running)
        {
            return;
        }

        double time = AudioSettings.dspTime;

        if (time + 1.0f > nextEventTime)
        {
            
            audioSources[flip].clip = clips[flip];
            audioSources[flip].PlayScheduled(nextEventTime);

            Debug.Log("Scheduled source " + flip + " to start at time " + nextEventTime);

           
            nextEventTime += 60.0f / bpm * numBeatsPerSegment;

            // Flip between two audio sources so that the loading process of one does not interfere with the one that's playing out
            flip = 1 - flip;
        }
    }
}