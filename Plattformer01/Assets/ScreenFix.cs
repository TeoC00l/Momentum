using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFix : MonoBehaviour
{
    Resolution res;

    void Start()
    {
        res = Screen.currentResolution;
        if (res.refreshRate == 60)
            QualitySettings.vSyncCount = 1;
        if (res.refreshRate == 144)
            QualitySettings.vSyncCount = 2;
        print(QualitySettings.vSyncCount);
    }
}