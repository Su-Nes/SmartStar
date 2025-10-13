using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnParent : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform parent;
    
    private void Start()
    {
        if (parent == null)
            parent = transform;
        
        Instantiate(prefab, parent.position, Quaternion.identity, parent);
    }
}
