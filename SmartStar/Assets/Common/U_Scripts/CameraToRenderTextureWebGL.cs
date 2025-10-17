using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraToRenderTextureWebGL : MonoBehaviour
{
    [SerializeField] private RenderTexture renderTexture;
    private Camera cam;
    
    private void Awake()
    {
        cam = GetComponent<Camera>();
        Camera.onPreRender += AssignRenderTexture;
        Camera.onPostRender += UnassignRenderTexture;
    }

    private void FixedUpdate()
    {
        cam.Render();
    }

    private void AssignRenderTexture(Camera funcCamera)
    {
        cam.targetTexture = renderTexture;
    }

    private void UnassignRenderTexture(Camera funcCamera)
    {
        cam.targetTexture = null;
    }
}
