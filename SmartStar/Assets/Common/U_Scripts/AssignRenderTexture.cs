using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class AssignRenderTexture : MonoBehaviour
{
    [SerializeField] private RenderTexture texture;
    
    private void Start()
    {
        GetComponent<RawImage>().texture = texture;
    }
}
