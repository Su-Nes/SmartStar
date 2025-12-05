using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnRemoveChildren : MonoBehaviour
{
    [SerializeField] private UnityEvent onChildrenClear, onDelay;
    [SerializeField] private float invokeDelay;
    private bool monitoring = true;
    private void Update()
    {
        if (transform.childCount == 0 && monitoring)
        {
            onChildrenClear.Invoke();
            StartCoroutine(InvokeAfterDelay());
            monitoring = false;
        }
    }

    private IEnumerator InvokeAfterDelay()
    {
        yield return new WaitForSeconds(invokeDelay);
        onDelay.Invoke();
    }
}
