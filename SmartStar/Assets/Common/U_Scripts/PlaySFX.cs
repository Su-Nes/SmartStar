using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    private void Awake()
    {
        
    }

    public void PlaySound(AudioClip clip)
    {
        SFXManager.Instance.PlaySFXClip(clip);
    }
}
