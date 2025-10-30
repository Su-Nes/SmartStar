using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCombinedVoiceLine : MonoBehaviour
{
    [SerializeField] private bool playOnEnable = true;
    [SerializeField] private SFXManager.VoiceCategory category = SFXManager.VoiceCategory.SFX;
    public AudioClip[] presetLines;

    public void OnEnable()
    {
        if(playOnEnable)
        {
            StartCoroutine(PlayVoiceLines(presetLines));
            playOnEnable = true;
        }
        
        playOnEnable = true; // fuck this is the worst spaghetti code and game I've ever fucking made jesus christ
    }

    public IEnumerator PlayVoiceLines(AudioClip[] clipArray)
    {
        foreach (AudioClip clip in clipArray)
        {
            SFXManager.Instance.PlaySFXClip(clip, category);
            yield return new WaitForSeconds(clip.length - .2f);
        }
    }
}
