using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void CorrectEvent();
    public static event CorrectEvent onCorrect;
    public delegate void IncorrectEvent();
    public static event IncorrectEvent onIncorrect;

    public enum EventTypes
    {
        none,
        onCorrect,
        onIncorrect
    }
    
    public static EventManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }else 
            Destroy(gameObject);
    }

    public void InvokeOnCorrect()
    {
        onCorrect?.Invoke();
    }

    public void InvokeOnIncorrect()
    {
        onIncorrect?.Invoke();
    }
}
