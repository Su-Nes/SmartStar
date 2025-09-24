using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    [SerializeField] private GameObject introScreen, mainScreen;

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    public void StartStation()
    {
        Time.timeScale = 1f;
        
        introScreen.SetActive(false);
        mainScreen.SetActive(true);
    }
}
