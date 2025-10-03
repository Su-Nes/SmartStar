using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class DraggableItemScript : MonoBehaviour
{
    [SerializeField] protected float forceToMouse, dragWhenDragging, dragOutOfDrag, dragWhenHeld;
    
    public string ItemKey = "default";
    
    protected Transform target, startPosition;
    private float startGravityScale;
    protected bool holdingDown, itemGrabbable = true;
    public bool HoldingDown => holdingDown;

    protected Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startGravityScale = rb.gravityScale; 
    }

    public virtual void StartDrag()
    {
        if (!itemGrabbable)
            return;
        
        holdingDown = true;
        RemoveTarget();
        
        rb.drag = dragWhenDragging;
    }
    
    public virtual void LeaveDrag()
    {
        holdingDown = false;
        rb.drag = dragOutOfDrag;
    }

    public virtual void SetTarget(Transform newTarget)
    {
        target = newTarget;
        //LeaveDrag();
        rb.drag = dragWhenHeld;
        rb.gravityScale = 0f;
    }

    public virtual void RemoveTarget()
    {
        target = null;
        rb.drag = dragOutOfDrag;
        rb.gravityScale = startGravityScale;
    }

    public virtual  void Update()
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
