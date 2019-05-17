using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSplash : MonoBehaviour
{
    [SerializeField]private float explosionFadeTime = 2f;

    void Awake()
    {
        Destroy(gameObject, explosionFadeTime);
    }

}
