using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

[RequireComponent(typeof(EventTrigger))]
public class Add3DForceFrom2DDrag : MonoBehaviour
{
    [SerializeField] private Rigidbody rbToLaunch;
    
    [SerializeField] private float dragTime, forceMult, rotationForce;
    private bool isDragging = true;
    private Vector2 dragStartPos, dragEndPos;
    
    
    public void OnStartDrag()
    {
        if (!isDragging)
        {
            StartCoroutine(DragTime());
            dragStartPos = Input.mousePosition;
            
            isDragging = true;
        }
    }

    public void OnEndDrag()
    {
        if (isDragging)
        {
            StopAllCoroutines();
            dragEndPos = Input.mousePosition;
            
            LaunchCube();
            isDragging = false;
        }
    }

    private void LaunchCube()
    {
        rbToLaunch.AddForce((dragEndPos - dragStartPos) * forceMult, ForceMode.Impulse);
        rbToLaunch.AddTorque(Random.insideUnitSphere.normalized * rotationForce, ForceMode.Impulse);
    }

    private IEnumerator DragTime()
    {
        yield return new WaitForSeconds(dragTime);
        
        isDragging = false;
    }
}
