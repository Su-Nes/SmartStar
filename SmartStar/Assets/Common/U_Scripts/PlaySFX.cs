using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    public void PlaySound(AudioClip clip)
    {
        SFXManager.Instance.PlaySFXClip(clip);
    }
    
    public void PlaySFXForCorrect()
    {
        SFXManager.Instance.PlayCorrectSound();
    }

    public void PlaySFXForIncorrect()
    {
        SFXManager.Instance.PLayIncorrectSound();
    }
}
