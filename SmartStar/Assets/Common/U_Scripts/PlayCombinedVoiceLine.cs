using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCombinedVoiceLine : MonoBehaviour
{
    [SerializeField] private SFXManager.VoiceCategory category = SFXManager.VoiceCategory.SFX;
    [SerializeField] private AudioClip[] onEnableLines;

    private void OnEnable()
    {
        if(onEnableLines.Length > 0)
        {
            StartCoroutine(PlayVoiceLines(onEnableLines));
        }
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
