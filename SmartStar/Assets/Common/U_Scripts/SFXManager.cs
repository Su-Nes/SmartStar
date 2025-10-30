using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    public enum VoiceCategory
    {
        SFX,
        VoiceLine
    }

    [SerializeField] private GameObject SFXObject;

    [SerializeField] private AudioClip[] universalCorrectSounds, universalIncorrectSounds;

    private bool audioReceiveEventInvoked, audioStoppedEventInvoked;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    private void Update()
    {
        if (transform.childCount > 0)
        {
            audioStoppedEventInvoked = false;
            EventManager.Instance.InvokeOnAudioStart();
        }else if (!audioStoppedEventInvoked)
        {
            audioStoppedEventInvoked = true;
            EventManager.Instance.InvokeOnAudioStop();
        }
    }

    public void PlayCorrectSound()
    {
        PlayRandomSFX(universalCorrectSounds, VoiceCategory.VoiceLine);
    }

    public AudioClip GetRandomCorrectSound()
    {
        return universalCorrectSounds[Random.Range(0, universalCorrectSounds.Length)];
    }

    public void PlayIncorrectSound()
    {
        PlayRandomSFX(universalIncorrectSounds, VoiceCategory.VoiceLine);
    }

    public AudioClip GetRandomIncorrectSound()
    {
        return universalIncorrectSounds[Random.Range(0, universalIncorrectSounds.Length)];
    }
    
    public void PlayRandomSFX(AudioClip[] audioClips, Vector3 spawnPosition, float volume)
    {
        PlaySFXClip(audioClips[Random.Range(0, audioClips.Length)], spawnPosition, volume);
    }
    
    public void PlayRandomSFX(AudioClip[] audioClips, VoiceCategory category)
    {
        PlaySFXClip(audioClips[Random.Range(0, audioClips.Length)], category);
    }
    
    public void PlaySFXClip(AudioClip audioClip, Vector3 spawnPosition, float volume = .75f, float minPitch = 1f, float maxPitch = 1f)
    {
        //EventManager.Instance.InvokeOnAudioStart();
        print("seems like this gets called 0.0");
        AudioSource audioSource =
            Instantiate(SFXObject.GetComponent<AudioSource>(), spawnPosition, Quaternion.identity, transform);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.gameObject.name = $"{audioClip.name} SFX";
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
        
        //EventManager.Instance.Invoke(nameof(EventManager.InvokeOnAudioStop), clipLength);
    }
    
    public void PlaySFXClip(AudioClip audioClip, VoiceCategory category = VoiceCategory.SFX, float volume = .75f, float minPitch = 1f, float maxPitch = 1f)
    {
        if (audioClip == null)
        {
            Debug.LogError("SFX Clip is null");
            return;
        }
        
        AudioSource audioSource;

        switch (category)
        {
            case VoiceCategory.SFX:
                audioSource = Instantiate(SFXObject.GetComponent<AudioSource>(), Vector3.zero, Quaternion.identity);
                audioSource.volume = volume;
                audioSource.gameObject.name = $"{audioClip.name} SFX";
                break;
            // voice lines get parented to this object, so the event manager can read voice lines starting and ending
            case VoiceCategory.VoiceLine:
                audioSource = Instantiate(SFXObject.GetComponent<AudioSource>(), Vector3.zero, Quaternion.identity, transform);
                audioSource.volume = 1f; // because poļuka voice is a lil quiet
                audioSource.gameObject.name = $"{audioClip.name} Voice Line";
                break;
            
            default:
                audioSource = Instantiate(SFXObject.GetComponent<AudioSource>(), Vector3.zero, Quaternion.identity);
                audioSource.volume = volume;
                audioSource.gameObject.name = $"{audioClip.name} SFX";
                break;
        }
        
        audioSource.clip = audioClip;
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
