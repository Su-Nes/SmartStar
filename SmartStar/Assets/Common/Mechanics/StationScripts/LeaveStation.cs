using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaveStation : MonoBehaviour
{
    [SerializeField] private int stationIndex;
    private void Start()
    {
        if (TryGetComponent(out Button button))
        {
            button.onClick.AddListener(StationSelector.Instance.CloseStations);
            button.onClick.AddListener(SetStationComplete);
        }
    }

    public void SetStationComplete()
    {
        DataPersistenceManager.Instance.SetStationAsComplete(stationIndex);
    }
}
