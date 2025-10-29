using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItemBoring : DraggableItemScript
{
     private void Start()
     {
          startPosition = new GameObject().transform;
          startPosition.name = $"startPosition of {name}";
          startPosition.position = transform.position;
          startPosition.SetParent(transform.parent);
          
          SetTarget(startPosition);

          gameObject.layer = 7;
     }

     public override void LeaveDrag()
     {
          base.LeaveDrag();
          
          rb.drag = dragWhenHeld;
          holdingDown = false;
     }
     
     public override void StartDrag(PointerEventData eventData)
     {
          if (!itemGrabbable)
               return;
              
          holdingDown = true;
          
          if(target != startPosition)
               SetTarget(startPosition);
          
          onStartDrag.Invoke();
        
          rb.drag = dragWhenDragging;
     }
     
     public override void SetTarget(Transform newTarget)
     {
          target = newTarget;
          rb.drag = dragWhenHeld;
          rb.gravityScale = 0f;
     }
     
     public override void RemoveTarget()
     {
          SetTarget(startPosition);
          rb.drag = dragWhenHeld;
          rb.gravityScale = 0f;
     }

     public override void Update()
     {    
          if (!holdingDown)
          {
               rb.AddForce((target.position - transform.position) * (forceToMouse * Time.deltaTime * Vector2.Distance(transform.position, target.position)));
          }
          else
          {
               if(target == startPosition)
                    rb.AddForce((Input.mousePosition - transform.position) * (forceToMouse * Time.deltaTime * Vector2.Distance(transform.position, Input.mousePosition)));
               else 
                    rb.AddForce((Input.mousePosition - transform.position) * (forceToMouse * Time.deltaTime * Vector2.Distance(transform.position, target.position)));
          }
        
          if(Input.GetMouseButtonUp(0))
               LeaveDrag();
     }
}
