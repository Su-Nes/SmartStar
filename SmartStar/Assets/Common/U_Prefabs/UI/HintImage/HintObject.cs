using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HintObject : MonoBehaviour
{
    [SerializeField] private float timeUntilHint = 10f;
    private float timer;

    [SerializeField] private Button[] buttonsThatResetHintTimer;
    [SerializeField] private EventTrigger[] eventTriggersThatResetHintTimer;
    [SerializeField] private UnityEvent onEnableHint, onDisableHint;

    private bool timerActive = true;
    

    private void Awake()
    {
        onDisableHint.Invoke();
        
        foreach (Button button in buttonsThatResetHintTimer)
        {
            button.onClick.AddListener(ResetTimer);
        }

        foreach (EventTrigger eventTrigger in eventTriggersThatResetHintTimer)
        {
            eventTrigger.AddListener(EventTriggerType.PointerDown, ResetTimer);
        }
    }

    private void Update()
    {
        if (!timerActive)
            return;
        
        timer += Time.deltaTime;

        if (timer >= timeUntilHint)
        {
            onEnableHint.Invoke();
        }
    }

    public void ActiveTimer(bool state)
    {
        timerActive = state;
    }

    private void ResetTimer()
    {
        onDisableHint.Invoke();
        timer = 0f;
    }
    private void ResetTimer(PointerEventData eventData)
    {
        onDisableHint.Invoke();
        timer = 0f;
    }
}
