using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCombinedVoiceLine : MonoBehaviour
{
    [SerializeField] private AudioClip voiceLineFirstHalf;
    
    public IEnumerator PlayVoiceWithClip(AudioClip clip)
    {
        SFXManager.Instance.PlaySFXClip(voiceLineFirstHalf);
        
        yield return new WaitForSeconds(clip.length);
        
        SFXManager.Instance.PlaySFXClip(clip);
    }
}
