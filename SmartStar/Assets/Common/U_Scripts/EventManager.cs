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
    
    public delegate void AudioStart();
    public static event AudioStart onAudioStart;
    public delegate void AudioStop();
    public static event AudioStop onAudioStop;

    public enum EventTypes
    {
        none,
        onCorrect,
        onIncorrect,
        onAudioStart,
        onAudioStop
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

    public void InvokeOnAudioStart()
    {
        onAudioStart?.Invoke();
    }

    public void InvokeOnAudioStop()
    {
        onAudioStop?.Invoke();
    }
}
