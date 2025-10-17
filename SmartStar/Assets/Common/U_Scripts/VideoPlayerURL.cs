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
    [SerializeField] private UnityEvent onVideoPrepared;
    [SerializeField] private RenderTexture targetTexture;
    [SerializeField] private RawImage rawImage;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        rawImage.gameObject.SetActive(false);
        
        videoPlayer.url = URL;
        videoPlayer.playOnAwake = false;
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
    }
}
