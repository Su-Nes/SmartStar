using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DotProductReader : MonoBehaviour
{
    [SerializeField] private Vector3 comparableVector;
    [SerializeField] private bool constantMonitoring;
    [Range(0f, 1f)]
    [SerializeField] private float detectionLeniency;
    [SerializeField] private UnityEvent onProductIs1;
    private bool hasInvoked;
    
    
    private void Update()
    {
        if (!constantMonitoring)
            return;
        
        if (Vector3.Dot(comparableVector, transform.up) < 1f - detectionLeniency)
        {
            if(!hasInvoked)
                onProductIs1.Invoke();
            hasInvoked = true;
            print($"{name} has been invoked.");
        }
        else
        {
            hasInvoked = false;
        }
    }

    public float GetProduct()
    {
        return Vector3.Dot(comparableVector, transform.up);
    }
}
