using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonPlaySFX : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }
    
    private void PlaySound()
    {
        SFXManager.Instance.PlaySFXClip(audioClip);
    }
}
