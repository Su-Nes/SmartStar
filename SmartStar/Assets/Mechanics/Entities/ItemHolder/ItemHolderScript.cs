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
    private DraggableItemScript currentHeldItem, itemLeaving;
    private bool holderActive = true, itemCanExit;
    
    [SerializeField] private UnityEvent onCorrectKey;
    [SerializeField] private UnityEvent<string> onGetItem;
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print($"{collision.gameObject.name} entered collider");

        if (!holderActive)
            return;
        
        if (collision.gameObject.CompareTag(targetTag))
        {
            HoldItem(collision.gameObject.GetComponent<DraggableItemScript>());
        }
    }

    private void HoldItem(DraggableItemScript item)
    {
        StartCoroutine(ItemLeaveBuffer()); // disallow new item to leave the holder for a little bit
            
        itemLeaving = null;

        if (currentHeldItem != null)
        {
            RemoveHeldItem(); // remove current held item to hold the new one
        }
            
        currentHeldItem = item;
        currentHeldItem.HoldingDown = false;
        currentHeldItem.SetTarget(transform);
            
        onGetItem.Invoke(currentHeldItem.ItemKey);
            
        if (currentHeldItem.ItemKey == targetKey)
        {
            onCorrectKey.Invoke();
            //print("event invoked");
        }
        
        print($"{currentHeldItem.name} holding down, {currentHeldItem.target.name}");
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!itemCanExit)
            return;
        
        print($"{collision.gameObject.name} left collider");
        if (!holderActive)
            return;

        if (collision.gameObject.CompareTag(targetTag))
        {
            RemoveHeldItem();
        }
    }

    private IEnumerator ItemLeaveBuffer()
    {
        itemCanExit = false;
        yield return new WaitForSeconds(itemLeaveBuffer);
        itemCanExit = true;
    }

    private void RemoveHeldItem()
    {
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
