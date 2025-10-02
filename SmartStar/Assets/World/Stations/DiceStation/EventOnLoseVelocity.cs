using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class EventOnLoseVelocity : MonoBehaviour
{
    [SerializeField] private float velocityMinimum = .1f, monitoringDelay = .5f, detectionDelay = 1f;
    [SerializeField] private UnityEvent onLoseVelocity;
    private bool canInvoke;
    
    private Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.velocity.magnitude <= velocityMinimum && rb.angularVelocity.magnitude <= velocityMinimum && canInvoke)
        {
            Invoke(nameof(InvokeEvent), detectionDelay);
            canInvoke = false;
        }
    }

    public void SetActive(bool active)
    {
        StartCoroutine(EnableMonitoring(active));
    }

    private void InvokeEvent()
    {
        onLoseVelocity.Invoke();
    }

    private IEnumerator EnableMonitoring(bool active)
    {
        yield return new WaitForSeconds(monitoringDelay);
        canInvoke = active;
    }
}
