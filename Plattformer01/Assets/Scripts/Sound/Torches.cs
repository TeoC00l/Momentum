using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torches : MonoBehaviour
{
    private AudioSource source;
    public AudioClip flame1;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = flame1;
        source.loop = true;
        source.time = Random.Range(0, flame1.length);
        source.Play();
    }

    void Update()
    {
    }
}
