using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSplash : MonoBehaviour
{
    public float explosionFadeTime = 2f;

    // Start is called before the first frame update

    // Update is called once per frame
    void Awake()
    {
        Destroy(gameObject, explosionFadeTime);
    }

}
