using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SleighControls : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f, lerpValue = .07f;
    private Vector3 targetPos;


    private void Start()
    {
        targetPos.x = transform.position.x;
        targetPos.y = transform.position.y;
    }

    private void Update()
    {
        targetPos.y = Input.mousePosition.y;
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpValue);
    }
}
