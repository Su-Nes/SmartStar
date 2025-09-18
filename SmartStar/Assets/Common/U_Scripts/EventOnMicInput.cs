using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnMicInput : MonoBehaviour
{
    [SerializeField] private float minimumTriggerVolume = .1f;
    
    [SerializeField] private UnityEvent volumeEvent;
    
    private void Update()
    {
        if(FakeVoiceRecognition.MicLoudness > minimumTriggerVolume)
            volumeEvent.Invoke();
    }
}
