using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EventTrigger))]
public class DraggableItemScript : MonoBehaviour
{
    [SerializeField] protected float forceToMouse, dragWhenDragging, dragOutOfDrag, dragWhenHeld, scaleMultWhenHeld, scaleLerp;
    
    public string ItemKey = "default";
    
    public Transform target, startPosition;

    private Vector3 heldScale, startScale;
    private float startGravityScale;
    protected bool holdingDown, isHeld, itemGrabbable = true;
    public bool HoldingDown
    {
        get => holdingDown;
        set => holdingDown = value;
    }

    public bool IsHeld
    {
        get => isHeld;
        set => isHeld = value;
    }

    protected Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startGravityScale = rb.gravityScale; 
        
        startScale = transform.localScale;
        heldScale = transform.localScale * scaleMultWhenHeld;
        
        EventTrigger dragEvents =  GetComponent<EventTrigger>();
        dragEvents.AddListener(EventTriggerType.PointerDown, StartDrag);
    }

    public virtual void StartDrag(PointerEventData eventData)
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

    public virtual void Update()
    {
        if (target && !holdingDown)
        {
            rb.AddForce((target.position - transform.position) * (forceToMouse * Time.deltaTime * Vector2.Distance(transform.position, target.position)));
        }else if (holdingDown)
        { 
            rb.AddForce((Input.mousePosition - transform.position) * (forceToMouse * Time.deltaTime * Vector2.Distance(transform.position, Input.mousePosition)));
        }
        
        if(Input.GetMouseButtonUp(0) && target == null)
            LeaveDrag();
    }

    private void FixedUpdate()
    {
        if (isHeld)
        {
            if (Vector3.Distance(transform.localScale, heldScale) > .05f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, heldScale, scaleLerp);
            }
        
            transform.localScale = heldScale;
        }
        else
        {
            if (Vector3.Distance(transform.localScale, startScale) > .05f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, startScale, scaleLerp);
            }
        
            transform.localScale = startScale;
        }
    }

    public void SetItemActivity(bool state)
    {
        itemGrabbable = state;
    }
}
