using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [SerializeField] private GameObject SFXObject;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }
    
    public void PlayRandomSFX(AudioClip[] audioClips, Vector3 spawnPosition, float volume)
    {
        PlaySFXClip(audioClips[Random.Range(0, audioClips.Length)], spawnPosition, volume);
    }
    
    public void PlaySFXClip(AudioClip audioClip, Vector3 spawnPosition, float volume = .75f, float minPitch = 1f, float maxPitch = 1f)
    {
        AudioSource audioSource =
            Instantiate(SFXObject.GetComponent<AudioSource>(), spawnPosition, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        StartCoroutine(RemoveAudioSource(audioSource.gameObject, clipLength));
    }
    
    public void PlaySFXClip(AudioClip audioClip, float volume = .75f, float minPitch = 1f, float maxPitch = 1f)
    {
        AudioSource audioSource =
            Instantiate(SFXObject.GetComponent<AudioSource>(), Vector3.zero, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        StartCoroutine(RemoveAudioSource(audioSource.gameObject, clipLength));
    }

    private IEnumerator RemoveAudioSource(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        if(obj != null)
            Destroy(obj);
    }
}
