using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayAnim : MonoBehaviour
{
    [SerializeField] private bool playOnEnable = true;
    [SerializeField] private string animName;
    
    [SerializeField] private EventManager.EventTypes playOnEvent;
    
    private void OnEnable()
    {
        if(playOnEnable)
            PlayAssignedAnim();

        switch (playOnEvent)
        {
            case EventManager.EventTypes.onCorrect:
                EventManager.onCorrect += PlayAssignedAnim;
                break;
            
            case EventManager.EventTypes.onIncorrect:
                EventManager.onIncorrect += PlayAssignedAnim;
                break;
        }
    }
    
    private void OnDisable()
    {
        switch (playOnEvent)
        {
            case EventManager.EventTypes.onCorrect:
                EventManager.onCorrect -= PlayAssignedAnim;
                break;
            
            case EventManager.EventTypes.onIncorrect:
                EventManager.onIncorrect -= PlayAssignedAnim;
                break;
        }
    }

    public void PlayAssignedAnim()
    {
        GetComponent<Animator>().Play(animName);
    }
    
    public void PlayAnimWithName(string animationName)
    {
        GetComponent<Animator>().Play(animationName);
    }
}
