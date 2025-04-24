using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceSettings : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 0;
        DontDestroyOnLoad(gameObject); // Keeps it alive across scenes if needed
    }
}
