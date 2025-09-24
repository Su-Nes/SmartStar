using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class ItemHolderScript : MonoBehaviour
{
    [SerializeField] private string targetTag = "Item", targetKey = "default";
    private DraggableItemScript currentHeldItem;
    private bool holderOccupied;
    
    [SerializeField] private UnityEvent eventOnCorrectKey;
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            if (holderOccupied)
                currentHeldItem.RemoveTarget(); // remove current held item to hold the new one
                
            holderOccupied = true;
            currentHeldItem = collision.GetComponent<DraggableItemScript>();
            currentHeldItem.SetTarget(transform);
            
            if (collision.GetComponent<DraggableItemScript>().ItemKey == targetKey)
            {
                eventOnCorrectKey.Invoke();
                print("event invoked");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            holderOccupied = false;
            currentHeldItem.RemoveTarget();
            currentHeldItem = null;
        }
    }
}
