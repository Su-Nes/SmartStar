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

          gameObject.layer = 7;
     }

     public override void Update()
     {
          base.Update();

          if (target == null && !holdingDown)
          {
               SetTarget(startPosition);
          }
     }
}
