using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TMP_Text))]
public class TextCounter : MonoBehaviour
{
    [SerializeField] private string textBefore;
    public int counter;
    [SerializeField] private int numForEvent;
    [SerializeField] private UnityEvent onReachedNumber;

    private void Start()
    {
        AddValue(0);
    }

    public void AddValue(int value)
    {
        counter += value;
        GetComponent<TMP_Text>().text = $"{textBefore}{counter.ToString()}";
        
        if(counter == numForEvent)
            onReachedNumber.Invoke();
    }
}
