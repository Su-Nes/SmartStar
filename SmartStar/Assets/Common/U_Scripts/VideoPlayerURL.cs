using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoPlayerURL : MonoBehaviour
{
    [SerializeField] private string URL;
    private VideoPlayer videoPlayer;
    [SerializeField] private UnityEvent onVideoPrepared, onVideoEnded;
    [SerializeField] private RenderTexture targetTexture;
    [SerializeField] private RawImage rawImage;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        rawImage.gameObject.SetActive(false);

        gameObject.AddComponent<AudioSource>();
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1.0f;
        
        videoPlayer.url = URL;
        videoPlayer.playOnAwake = false;
        videoPlayer.controlledAudioTrackCount = 1;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
        
        videoPlayer.Prepare();

        videoPlayer.prepareCompleted += OnVideoPrepared;
    }

    private void OnVideoPrepared(VideoPlayer source)
    {
        rawImage.gameObject.SetActive(true);
        rawImage.texture = targetTexture;
        videoPlayer.targetTexture = targetTexture;
        
        videoPlayer.Play();
        onVideoPrepared.Invoke();

        StartCoroutine(VideoTimer((float)videoPlayer.length));
    }

    private IEnumerator VideoTimer(float time)
    {
        yield return new WaitForSeconds(time);
        onVideoEnded.Invoke();
    }
}
