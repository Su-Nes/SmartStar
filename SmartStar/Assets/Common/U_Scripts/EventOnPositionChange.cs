using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnPositionChange : MonoBehaviour
{
    [SerializeField] private UnityEvent onPositionChange;

    private Vector3 lastPosition;
    
    private void Update()
    {
        if (transform.position != lastPosition)
        {
            onPositionChange.Invoke();
        }
        
        lastPosition = transform.position;
    }
}
