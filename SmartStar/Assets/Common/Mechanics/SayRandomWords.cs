using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class SayRandomWords : MonoBehaviour
{
    [SerializeField] private string[] words;

    private void Start()
    {
        SetRandomWord();
    }

    public void SetRandomWord()
    {
        GetComponent<TMP_Text>().text = $"Say\n{words[Random.Range(0, words.Length)]}";
    }
}
