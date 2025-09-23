using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationSelector : MonoBehaviour
{
    public static StationSelector Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectStation(string stationName)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(child.name.Contains(stationName));
        }
    }

    public void CloseStations()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
