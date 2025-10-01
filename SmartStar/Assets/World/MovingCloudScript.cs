using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCloudScript : MonoBehaviour
{
    [SerializeField] private float leftXLimit, rightXLimit, spawnX;
    [SerializeField] private Vector3 moveSpeed;

    private void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime, Space.World);
        
        if (transform.position.x < leftXLimit || transform.position.x > rightXLimit)
        {
            transform.position = new Vector3(spawnX, transform.position.y, transform.position.z);
        }
    }
}
