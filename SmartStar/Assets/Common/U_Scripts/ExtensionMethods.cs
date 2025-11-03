using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class ExtensionMethods
{
    public static Transform ScaledObject;
    public static Vector3 ScaledObjectStartScale;
    
    public static void AddListener (this EventTrigger trigger, EventTriggerType eventType, System.Action<PointerEventData> listener)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener(data => listener.Invoke((PointerEventData)data));
        trigger.triggers.Add(entry);
    }
    
    public static IEnumerator LerpScale(Transform transformToScale, float scaleMult, float lerpValue)
    {
        Vector3 targetScale = transformToScale.localScale * scaleMult;

        while (Vector3.Distance(transformToScale.localScale, targetScale) > .05f)
        {
            transformToScale.localScale = Vector3.Lerp(transformToScale.localScale, targetScale, lerpValue);
            yield return new WaitForFixedUpdate();
        }
        
        transformToScale.localScale = targetScale;
    }

    public static IEnumerator LerpScale(Transform transformToScale, Vector3 targetScale, float lerpValue)
    {
        while (Vector3.Distance(transformToScale.localScale, targetScale) > .05f)
        {
            transformToScale.localScale = Vector3.Lerp(transformToScale.localScale, targetScale, lerpValue);
            yield return new WaitForFixedUpdate();
        }
        
        transformToScale.localScale = targetScale;
    }
}
