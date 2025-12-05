using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CrossfadeImageWithChildren : MonoBehaviour
{
    [SerializeField] private float duration = 2f;
    
    public void StartCrossfade(float alpha)
    {
        GetComponent<Image>().CrossFadeAlpha(alpha, duration, false);
        foreach (Image image in transform.GetComponentsInChildren<Image>())
        { 
            image.CrossFadeAlpha(alpha, duration, false);
        }
    }
}
