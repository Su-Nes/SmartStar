using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationSelector : MonoBehaviour
{
    public static StationSelector Instance;
    
    [SerializeField] private GameObject[] stations;
    private string activeStation;


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
        
        CloseStations();
    }
    
    public void CreateStationWithName(string stationName)
    {
        CloseStations();
        
        foreach (GameObject station in stations)
        {
            if (station.name.ToLower().Contains(stationName.ToLower()))
            {
                Instantiate(station, transform);
                activeStation = station.name;
                return;
            }
            
            Debug.LogError(stationName + " doesn't exist");
        }
    }

    public void CloseStations()
    {
        activeStation = "";
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
