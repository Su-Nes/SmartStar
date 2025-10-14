using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnParent : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform parent;
    [SerializeField] private bool instantiateOnStart = true;
    
    private void Start()
    {
        if (parent == null)
            parent = transform;
        
        if(instantiateOnStart)
            TriggerInstantiate();
    }

    public void TriggerInstantiate()
    {
        Instantiate(prefab, parent.position, Quaternion.identity, parent);
    }
}
