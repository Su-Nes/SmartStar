using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItemScript : MonoBehaviour
{
    public void DragObject(BaseEventData eventData)
    {
        transform.position = new Vector2(transform.position.x + Input.GetAxis("Mouse X"), transform.position.y + Input.GetAxis("Mouse Y"));
    }
}
