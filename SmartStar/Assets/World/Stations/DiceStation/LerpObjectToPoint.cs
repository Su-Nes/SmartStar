using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpObjectToPoint : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float lerpValue = .1f;
    [SerializeField] private Vector3 offsetPosition;

    private Transform startPosition, startLookRotation;

    private void Awake()
    {
        startPosition = new GameObject().transform;
        startPosition.SetParent(transform.parent);
        startPosition.name = "DiceCameraStartPosition";
        startPosition.position = transform.position;
        startPosition.rotation = transform.rotation;
            
        startLookRotation = new GameObject().transform;
        startLookRotation.SetParent(transform.parent);
        startLookRotation.name = "DiceCameraStartLookTarget";
        startLookRotation.position = startPosition.position + startPosition.forward * 10f;
    }

    public void LerpToOrigin()
    {
        StartCoroutine(LerpPosition(startPosition, Vector3.zero));
        StartCoroutine(LerpRotation(startLookRotation));
    }
    
    public void LerpToTransform(Transform target)
    {
        StartCoroutine(LerpPosition(target, offsetPosition));
        StartCoroutine(LerpRotation(target));
    }
    
    private IEnumerator LerpPosition(Transform target, Vector3 offset)
    {
        if (!gameObject.activeSelf)
            StopAllCoroutines();
        
        while (Vector3.Distance(transform.position, target.position + offset) > 1f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpValue);
            yield return null;
        }
    }

    private IEnumerator LerpRotation(Transform target)
    {
        if (!gameObject.activeSelf)
            StopAllCoroutines();
        
        while (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(target.position - transform.position, Vector3.up)) > 3f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position, Vector3.up), lerpValue);
            yield return null;
        }
    }
}
