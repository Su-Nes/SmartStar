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
    private DraggableItemScript currentHeldItem, itemLeaving;
    private bool holderOccupied, holderActive = true;
    
    [SerializeField] private UnityEvent onCorrectKey;
    [SerializeField] private UnityEvent<string> onGetItem;
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!holderActive)
            return;
        
        if (collision.gameObject.CompareTag(targetTag))
        {
            if (collision.gameObject.GetComponent<DraggableItemScript>() == itemLeaving)
                return;
            
            itemLeaving = null;
            
            if (holderOccupied)
                RemoveHeldItem(collision); // remove current held item to hold the new one
            
            holderOccupied = true;
            currentHeldItem = collision.GetComponent<DraggableItemScript>();
            currentHeldItem.SetTarget(transform);
            onGetItem.Invoke(currentHeldItem.ItemKey);
            
            if (collision.GetComponent<DraggableItemScript>().ItemKey == targetKey)
            {
                onCorrectKey.Invoke();
                //print("event invoked");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!holderActive)
            return;

        if (collision.gameObject.CompareTag(targetTag))
        {
            RemoveHeldItem(collision);
        }
        
        itemLeaving = collision.GetComponent<DraggableItemScript>();
    }

    private void RemoveHeldItem(Collider2D collision)
    {
        holderOccupied = false;
        if(currentHeldItem != null)
            currentHeldItem.RemoveTarget();
        currentHeldItem = null;
    }

    public void SetHolderActivity(bool state)
    {
        holderActive = state;
    }

    public void LockHeldItem()
    {
        if(currentHeldItem != null)
            currentHeldItem.SetItemActivity(false);
    }
}
