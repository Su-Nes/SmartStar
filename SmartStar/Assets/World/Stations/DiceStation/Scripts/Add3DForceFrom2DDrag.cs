using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

[RequireComponent(typeof(EventTrigger))]
public class Add3DForceFrom2DDrag : MonoBehaviour
{
    [SerializeField] private Rigidbody rbToLaunch;
    
    [SerializeField] private float dragTime, forceMult, rotationForce, minimalLaunchDrag;
    [SerializeField] private int launchCount = 1;
    
    [SerializeField] private UnityEvent onLaunch;
    
    private bool isDragging;
    private Vector2 dragStartPos, dragEndPos;
    
    
    public void OnStartDrag()
    {
        if (!isDragging)
        {
            StartCoroutine(DragTime());
            
            dragStartPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            
            isDragging = true;
        }
    }

    public void OnEndDrag()
    {
        if (isDragging)
        {
            StopAllCoroutines();
            dragEndPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            
            if((dragEndPos - dragStartPos).magnitude > minimalLaunchDrag) // minimal throw force
                LaunchCube();
            isDragging = false;
        }
    }

    private void LaunchCube()
    {print($"start drag: {dragStartPos}, end drag: {dragEndPos}, magnitude: {(dragEndPos - dragStartPos).magnitude}");
        if (launchCount > 0)
        {
            Vector3 mouseVector = dragEndPos - dragStartPos;
            Vector3 launchVector = new(mouseVector.x, Mathf.Abs(mouseVector.y));
            Vector3 perpendicular = new(mouseVector.y, 0f, -mouseVector.x);

            rbToLaunch.AddForce(launchVector * forceMult, ForceMode.Impulse);
            rbToLaunch.AddTorque(perpendicular * rotationForce, ForceMode.Impulse);
            
            onLaunch.Invoke();
            
            launchCount--;
        }
    }

    public void AddLaunches(int amount)
    {
        launchCount += amount;
    }

    private IEnumerator DragTime()
    {
        yield return new WaitForSeconds(dragTime);
        
        isDragging = false;
    }
}
