using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableItemBoring : DraggableItemScript
{
     private void Start()
     {
          startPosition = new GameObject().transform;
          startPosition.name = $"startPosition of {name}";
          startPosition.position = transform.position;
          
          SetTarget(startPosition);

          gameObject.layer = 7;
     }

     public override void LeaveDrag()
     {
          base.LeaveDrag();
          
          rb.drag = dragWhenHeld;
          holdingDown = false;
     }
     
     public override void StartDrag()
     {
          print($"{name} started dragging");
          if (!itemGrabbable)
               return;
        
          holdingDown = true;
          
          if(target != startPosition)
               SetTarget(startPosition);
        
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
