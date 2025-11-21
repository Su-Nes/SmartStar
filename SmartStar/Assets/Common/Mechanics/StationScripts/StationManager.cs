using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    [SerializeField] private GameObject introScreen, mainScreen, outroScreen;
    [SerializeField] private AudioClip stationSpecificMusic;
    [SerializeField] private float musicVolume = .1f;

    private void OnEnable()
    {
        //Time.timeScale = 0f;
        introScreen.SetActive(true);
        mainScreen.SetActive(false);
        outroScreen.SetActive(false);
        
        StartCoroutine(MusicManager.Instance.FadeIt(stationSpecificMusic, musicVolume));
    }

    public void StartStation()
    {
        Time.timeScale = 1f;
        
        introScreen.SetActive(false);
        mainScreen.SetActive(true);
    }

    public void ShowOutro()
    {
        //Time.timeScale = 0f;
        outroScreen.SetActive(true);
    }

    public void ExitStation()
    {
        Time.timeScale = 1f;
        if (transform.parent.TryGetComponent(out StationSelector stationSelector))
        {
            stationSelector.CloseStations();
        }
    }
}
