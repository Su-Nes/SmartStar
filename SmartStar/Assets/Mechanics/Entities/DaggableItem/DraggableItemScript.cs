using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class DraggableItemScript : MonoBehaviour
{
    [SerializeField] private float forceToMouse, dragWhenDragging, dragOutOfDrag, dragWhenHeld;
    
    public string ItemKey = "default";
    
    private Transform target;
    private bool holdingDown, itemGrabbable = true;
    public bool HoldingDown => holdingDown;

    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void StartDrag()
    {
        if (!itemGrabbable)
            return;
        
        RemoveTarget();
        
        holdingDown = true;
        rb.drag = dragWhenDragging;
    }
    
    public void LeaveDrag()
    {
        holdingDown = false;
        rb.drag = dragOutOfDrag;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        LeaveDrag();
        rb.drag = dragWhenHeld;
    }

    public void RemoveTarget()
    {
        target = null;
        rb.drag = dragOutOfDrag;
    }

    private void Update()
    {
        if (target)
        {
            rb.AddForce((target.position - transform.position) * (forceToMouse * Time.deltaTime * Vector2.Distance(transform.position, target.position)));
        }else if (holdingDown)
        { 
            rb.AddForce((Input.mousePosition - transform.position) * (forceToMouse * Time.deltaTime * Vector2.Distance(transform.position, Input.mousePosition)));
        }
        
        if(Input.GetMouseButtonUp(0) && target == null)
            LeaveDrag();
    }

    public void SetItemActivity(bool state)
    {
        itemGrabbable = state;
    }
}
