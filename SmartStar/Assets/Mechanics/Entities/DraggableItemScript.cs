using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class DraggableItemScript : MonoBehaviour
{
    [SerializeField] private float forceToMouse = 20f;
    
    private bool holdingDown;
    
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void StartDrag(BaseEventData eventData)
    {
        holdingDown = true;
    }
    
    public void LeaveDrag(BaseEventData eventData)
    {
        holdingDown = false;
    }

    private void Update()
    {
        if (holdingDown)
        {
            rb.AddForce((Input.mousePosition - transform.position) * (forceToMouse * Time.deltaTime * Vector2.Distance(transform.position, Input.mousePosition)));
        }
        
        if(Input.GetMouseButtonUp(0))
            holdingDown = false;
    }
}
