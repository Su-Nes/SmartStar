using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    [SerializeField] private SFXManager.VoiceCategory category = SFXManager.VoiceCategory.SFX;
    [SerializeField] private AudioClip audioOnEnable;

    private void OnEnable()
    {
        if(audioOnEnable != null)
            PlaySound(audioOnEnable);
    }

    public void PlaySound(AudioClip clip)
    {
        SFXManager.Instance.PlaySFXClip(clip, category);
    }
    
    public void PlaySFXForCorrect()
    {
        SFXManager.Instance.PlayCorrectSound();
    }

    public void PlaySFXForIncorrect()
    {
        SFXManager.Instance.PlayIncorrectSound();
    }
}
