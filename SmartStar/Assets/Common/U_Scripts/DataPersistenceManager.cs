using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager Instance;

    private Transform checkmarkHolder;
    public List<bool> stationCompletionList = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    public void SetStationAsComplete(int stationIndex)
    {
        stationCompletionList[stationIndex] = true;
    }
}
