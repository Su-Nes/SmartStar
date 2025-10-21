using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [SerializeField] private GameObject SFXObject;

    [SerializeField] private AudioClip[] universalCorrectSounds, universalIncorrectSounds;

    private bool eventInvoked;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    public void PlayCorrectSound()
    {
        PlayRandomSFX(universalCorrectSounds);
    }

    public void PlayIncorrectSound()
    {
        PlayRandomSFX(universalIncorrectSounds);
    }
    
    public void PlayRandomSFX(AudioClip[] audioClips, Vector3 spawnPosition, float volume)
    {
        PlaySFXClip(audioClips[Random.Range(0, audioClips.Length)], spawnPosition, volume);
    }
    
    public void PlayRandomSFX(AudioClip[] audioClips)
    {
        PlaySFXClip(audioClips[Random.Range(0, audioClips.Length)]);
    }
    
    public void PlaySFXClip(AudioClip audioClip, Vector3 spawnPosition, float volume = .75f, float minPitch = 1f, float maxPitch = 1f)
    {
        EventManager.Instance.InvokeOnAudioStart();
        
        AudioSource audioSource =
            Instantiate(SFXObject.GetComponent<AudioSource>(), spawnPosition, Quaternion.identity, transform);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            eventInvoked = true;

            EventManager.Instance.InvokeOnAudioStop();
        }
        else
        {
            EventManager.Instance.InvokeOnAudioStart();
        }
    }
    
    public void PlaySFXClip(AudioClip audioClip, float volume = .75f, float minPitch = 1f, float maxPitch = 1f)
    {
        EventManager.Instance.InvokeOnAudioStart();
        
        AudioSource audioSource =
            Instantiate(SFXObject.GetComponent<AudioSource>(), Vector3.zero, Quaternion.identity, transform);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
        
        EventManager.Instance.Invoke(nameof(EventManager.InvokeOnAudioStop), clipLength);
    }
}
