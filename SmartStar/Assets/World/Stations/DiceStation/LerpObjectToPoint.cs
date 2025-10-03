using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LerpObjectToPoint : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float lerpValuePosition = .1f, lerpValueRotation = .1f;
    [SerializeField] private Vector3 offsetPosition, offsetLookPosition;

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
        StopAllCoroutines();
        StartCoroutine(LerpToPosition(startPosition, Vector3.zero));
        StartCoroutine(LerpRotation(startLookRotation, Vector3.zero));
    }
    
    public void LerpToTransform(Transform target)
    {
        StopAllCoroutines();
        StartCoroutine(LerpToPosition(target, offsetPosition));
        StartCoroutine(LerpRotation(target, offsetLookPosition));
    }
    
    private IEnumerator LerpToPosition(Transform target, Vector3 offset)
    {
        if (!gameObject.activeSelf)
            StopAllCoroutines();
        
        while (Vector3.Distance(transform.position, target.position + offset) > 1f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpValuePosition);
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator LerpRotation(Transform target, Vector3 offset)
    {
        if (!gameObject.activeSelf)
            StopAllCoroutines();
        
        while (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(target.position + offset - transform.position, Vector3.up)) > 5f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position, Vector3.up), lerpValueRotation);
            yield return new WaitForFixedUpdate();
        }
    }
}
