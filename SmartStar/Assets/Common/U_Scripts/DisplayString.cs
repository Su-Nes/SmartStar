using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class DisplayString : MonoBehaviour
{
    [SerializeField] private string textBefore, textAfter;
    
    public void ShowStringTMP(string text)
    {
        GetComponent<TMP_Text>().text = $"{textBefore}{text}{textAfter}";
    }

    public void ShowStringRaw(string text)
    {
        GetComponent<TMP_Text>().text = text;
    }
}
