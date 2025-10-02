using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour
{
    [SerializeField] private Vector3 rotationOffset;
    public void SetPositionToTarget(Transform target)
    {
        transform.SetParent(target);
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.LookRotation(target.up, target.right);
    }
}
