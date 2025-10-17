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
    [SerializeField] private float itemLeaveBuffer = .5f;
    private DraggableItemScript currentHeldItem;
    private bool holderActive = true, itemCanExit;
    
    [SerializeField] private UnityEvent<ItemHolderScript> onCorrectKey, onWrongKey, onRemoveCorrectKey, onRemoveWrongKey;
    [SerializeField] private UnityEvent<string> onGetItem;
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!holderActive)
            return;
        
        if (collision.gameObject.CompareTag(targetTag) && !collision.gameObject.GetComponent<DraggableItemScript>().IsHeld)
        {
            HoldItem(collision.gameObject.GetComponent<DraggableItemScript>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!itemCanExit)
            return;
        
        if (!holderActive)
            return;

        if (collision.gameObject.CompareTag(targetTag))
        {
            RemoveHeldItem();
        }
    }
    
    private void HoldItem(DraggableItemScript item)
    {
        StartCoroutine(ItemLeaveBuffer()); // disallow new item to leave the holder for a little bit
        
        if (item.ItemKey == targetKey)
            onCorrectKey.Invoke(this);
        else
            onWrongKey.Invoke(this);
        
        if (currentHeldItem != null)
        {
            RemoveHeldItem(); // remove current held item to hold the new one
        }
            
        currentHeldItem = item;
        currentHeldItem.HoldingDown = false;
        currentHeldItem.IsHeld = true;
        currentHeldItem.SetTarget(transform);
        currentHeldItem.onGetHeld.Invoke();
        
        // events    
        onGetItem.Invoke(currentHeldItem.ItemKey);
    }
    
    private void RemoveHeldItem()
    {
        if (currentHeldItem != null)
        {
            if (currentHeldItem.ItemKey == targetKey)
                onRemoveCorrectKey.Invoke(this);
            else 
                onRemoveWrongKey.Invoke(this);
            
            currentHeldItem.RemoveTarget();
            currentHeldItem.onStopBeingHeld.Invoke();
        }
            
        currentHeldItem.IsHeld = false;
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
    
    private IEnumerator ItemLeaveBuffer()
    {
        itemCanExit = false;
        yield return new WaitForSeconds(itemLeaveBuffer);
        itemCanExit = true;
    }
}
