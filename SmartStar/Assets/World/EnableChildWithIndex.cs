using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableChildWithIndex : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private int onAwakeIndex;
    
    private void Awake()
    {
        if (parent == null)
            parent = transform.parent;
        
        EnableChild(onAwakeIndex);
    }

    public void EnableChild(int index)
    {
        if (onAwakeIndex < 0)
            return;

        int i = 0;
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(i ==  onAwakeIndex);
            i++;
        }
            
    }
}
