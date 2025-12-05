using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EventOnGlobalEvent : MonoBehaviour
{
    [SerializeField] private EventManager.EventTypes eventType;
    [SerializeField] private float invokeDelay;
    [SerializeField] private string triggerTargetName;
    [SerializeField] private UnityEvent onEventImmediate;
    [FormerlySerializedAs("onEvent")] [SerializeField] private UnityEvent onEventDelayed;
    [SerializeField] private UnityEvent<Collider2D> onTrigger;
    [SerializeField] private bool deleteTriggerObject;
    
    private bool hasEnterTrigger, hasExitTrigger;
    
    private void OnEnable()
    {
        switch (eventType)
        {
            case EventManager.EventTypes.onEnable:
                InvokeEvent();
                break;
            
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
            
            case EventManager.EventTypes.onTriggerEnter:
                CreateTrigger();
                hasEnterTrigger = true;
                break;
            
            case EventManager.EventTypes.onTriggerExit:
                CreateTrigger();
                hasExitTrigger = true;
                break;
            
            case EventManager.EventTypes.none:
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnDisable()
    {
        switch (eventType)
        {
            case EventManager.EventTypes.onDisable:
                InvokeEvent();
                break;
            
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
        }
    }

    public void InvokeEvent()
    {
        onEventImmediate.Invoke();
        StartCoroutine(DelayedEvent());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasEnterTrigger)
            return;
        
        InvokeOnMatchingName(other.gameObject.name);
        if (deleteTriggerObject)
        {
            Destroy(other.gameObject);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!hasExitTrigger)
            return;
        
        InvokeOnMatchingName(other.gameObject.name);
        if (deleteTriggerObject)
        {
            Destroy(other.gameObject);
        }
    }

    private void CreateTrigger()
    {
        if (!TryGetComponent(out Collider2D test))
        {
            Collider2D trigger = gameObject.AddComponent<BoxCollider2D>();
            trigger.isTrigger = true;
        }
        
        
    }

    private void InvokeOnMatchingName(string comparable)
    {
        if (comparable.ToLower().Contains(triggerTargetName.ToLower()))
        {
            InvokeEvent();
        }
    }

    private IEnumerator DelayedEvent()
    {
        yield return new WaitForSeconds(invokeDelay);
        onEventDelayed.Invoke();
    }
}
