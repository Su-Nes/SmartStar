using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MemoryCardScript : MonoBehaviour
{
    [SerializeField] private float flippedRotation, rotationLerp = .005f;
    [SerializeField] private UnityEvent onEnableFront, onEnableBack;
    public string Keyword;
    
    public bool IsLocked { set; get; }
    public bool IsFlipped { set; get; }
    
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(FlipCard);
    }
    
    private void FlipCard()
    {
        if (IsLocked)
            return;
        
        if (!IsFlipped)
        {
            StartCoroutine(RotateCardY(flippedRotation));
            IsLocked = true;
        }
    }

    public void UnflipCard()
    {
        if (IsFlipped)
        {
            StartCoroutine(RotateCardY(0f, true));
            IsFlipped = false;
            IsLocked = false;
        }
    }

    private IEnumerator RotateCardY(float YTarget, bool enableButtonAfterFlip = false)
    {
        GetComponent<Button>().interactable = false;
        
        float startY = transform.eulerAngles.y;
        while (true)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, YTarget, 0f), rotationLerp);

            // halfway through the rotation disable current card side and enable the other side
            if (Quaternion.Angle(transform.rotation, Quaternion.Euler(0f, YTarget, 0f)) < Mathf.Abs(startY - YTarget) / 2f)
            {
                if(IsFlipped)
                    onEnableFront.Invoke();
                else 
                    onEnableBack.Invoke();
            }

            if (Quaternion.Angle(transform.rotation, Quaternion.Euler(0f, YTarget, 0f)) < 3f)
                break;
            
            yield return null;
        }
        
        if (enableButtonAfterFlip)
            GetComponent<Button>().interactable = true;
        
        transform.rotation = Quaternion.Euler(0f, YTarget, 0f);
    }
}
