using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnParent : MonoBehaviour
{
    [SerializeField] private bool instantiateOnStart = true, useThisObjectsPosition = true;

    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform parent;
    
    private void Start()
    {
        if (parent == null)
            parent = transform;

        if (instantiateOnStart)
        {
            if (useThisObjectsPosition)
                TriggerInstantiate(parent, transform.position);
            else
                TriggerInstantiate();
        }
    }

    public void TriggerInstantiate()
    {
        Instantiate(prefab, parent.position, Quaternion.identity, parent);
    }
    
    public void TriggerInstantiate(Transform targetParent)
    {
        Instantiate(prefab, targetParent.position, Quaternion.identity, targetParent);
    }
    
    public void TriggerInstantiate(Transform targetParent, Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.identity, targetParent);
    }
}
