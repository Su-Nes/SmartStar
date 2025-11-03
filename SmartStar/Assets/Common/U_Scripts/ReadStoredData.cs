using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadStoredData : MonoBehaviour
{
    private DataPersistenceManager dataPersistanceManager;
    
    private void OnEnable()
    {
        dataPersistanceManager = DataPersistenceManager.Instance;
        EnableCheckmarks();
    }

    private void EnableCheckmarks()
    {
        int checkmarkIndex = 0;
        foreach (bool stationCompletion in dataPersistanceManager.stationCompletionList)
        {
            transform.GetChild(checkmarkIndex).gameObject.SetActive(stationCompletion);
            checkmarkIndex++;
        }
    }
}
