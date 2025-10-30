using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnGlobalEvent : MonoBehaviour
{
    [SerializeField] private EventManager.EventTypes eventType;
    [SerializeField] private float invokeDelay;
    [SerializeField] private UnityEvent onEvent;
    
    private void OnEnable()
    {
        switch (eventType)
        {
            case EventManager.EventTypes.onCorrect:
                EventManager.onCorrect += InvokeEvent;
                break;
            
            case EventManager.EventTypes.onIncorrect:
                EventManager.onIncorrect += InvokeEvent;
                break;
            
            case EventManager.EventTypes.onAudioStart:
                EventManager.onAudioStart += InvokeEvent;
                break;
            
            case EventManager.EventTypes.onAudioStop:
                EventManager.onAudioStop += InvokeEvent;
                break;
            
            case EventManager.EventTypes.none:
                break;
            
            case EventManager.EventTypes.onEnable:
                InvokeEvent();
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnDisable()
    {
        switch (eventType)
        {
            case EventManager.EventTypes.onCorrect:
                EventManager.onCorrect -= InvokeEvent;
                break;
            
            case EventManager.EventTypes.onIncorrect:
                EventManager.onIncorrect -= InvokeEvent;
                break;
            
            case EventManager.EventTypes.onAudioStart:
                EventManager.onAudioStart -= InvokeEvent;
                break;
            
            case EventManager.EventTypes.onAudioStop:
                EventManager.onAudioStop -= InvokeEvent;
                break;
            
            case EventManager.EventTypes.none:
                break;
            
            case EventManager.EventTypes.onDisable:
                InvokeEvent();
                break;
        }
    }

    public void InvokeEvent()
    {
        StartCoroutine(DelayedEvent());
    }

    private IEnumerator DelayedEvent()
    {
        yield return new WaitForSeconds(invokeDelay);
        onEvent.Invoke();
    }
}
