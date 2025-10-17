using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetFrameRate : MonoBehaviour
{
    [SerializeField] private int targetFrameRate;
    
    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        
        Application.targetFrameRate = targetFrameRate;
    }
}
