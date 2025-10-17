using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour
{
    [SerializeField] private Vector3 rotationOffset;
    [SerializeField] private bool createOrigin;
    private Transform origin;

    private void Awake()
    {
        if (createOrigin)
        {
            origin = new GameObject().transform;
            origin.position = transform.position;
            origin.rotation = transform.rotation;
            origin.parent = transform.parent;
        }
            
    }
    public void SetPositionToTarget(Transform target)
    {
        transform.SetParent(target);
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.LookRotation(target.up, target.right);
    }

    public void ReturnToOrigin(bool returnWithRotation = false)
    {
        transform.position = origin.position;
        if(returnWithRotation)
            transform.rotation = origin.rotation;
    }
}
