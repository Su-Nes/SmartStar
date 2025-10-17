using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(InstantiateOnParent))]
public class ComparisonStationManager : MonoBehaviour
{
    [SerializeField] private string[] objectTitles, objectDisplayNames;
    [SerializeField] private Sprite[] objectSprites;
    private string currentComparison, otherComparison;
    [SerializeField] private DisplayString[] titleTexts, comparisonTexts;
    [SerializeField] private Image[] objectDisplayImages;
    [SerializeField] private Transform yesButton, noButton;
    [SerializeField] private float answerDelay = 2.5f;
    [SerializeField] private DisplayString countdownText;
    [SerializeField] private UnityEvent onTrue, onFalse, onAnswerComplete;

    [SerializeField] private int turnCount = 10;
    [SerializeField] private bool objectivesCanRepeat;
    
    
    private void Start()
    {
        ComparisonStart();
        countdownText.ShowStringRaw(turnCount.ToString());
    }

    private void ComparisonStart(string lastUsedComparison = "")
    {
        int randomIndex = Random.Range(0, objectTitles.Length);
        currentComparison = objectTitles[randomIndex];
        if(!objectivesCanRepeat && lastUsedComparison == currentComparison)
            ComparisonStart(currentComparison);
        
        foreach (DisplayString display in titleTexts)
        {
            display.ShowStringTMP(currentComparison);
        }

        foreach (DisplayString display in comparisonTexts)
        {
            display.ShowStringTMP(objectDisplayNames[randomIndex]);
        }

        foreach (Image img in objectDisplayImages)
        {
            img.sprite = objectSprites[randomIndex];
        }
    }

    public void AssignComparison(string comparison)
    {
        otherComparison = comparison;
    }

    public void SpendTurn()
    {
        turnCount--;
        countdownText.ShowStringRaw(turnCount.ToString());
    }

    public void AnswerYes()
    {
        if (currentComparison.ToLower().Contains(otherComparison.ToLower()))
        {
            StartCoroutine(Answered(true, true));
            
            EventManager.Instance.InvokeOnCorrect();
        }
        else
        {
            StartCoroutine(Answered(false));
            GetComponent<InstantiateOnParent>().TriggerInstantiate(yesButton);
        }
        
        EndCheck();
    }

    public void AnswerNo()
    {
        if (!currentComparison.ToLower().Contains(otherComparison.ToLower()))
        {
            StartCoroutine(Answered(true));

            EventManager.Instance.InvokeOnCorrect();
        }
        else
        {
            StartCoroutine(Answered(false));
            GetComponent<InstantiateOnParent>().TriggerInstantiate(noButton);
        }
        
        EndCheck();
    }
    
    private void EndCheck()
    {
        countdownText.ShowStringRaw(turnCount.ToString());
        if (turnCount <= 0)
        {
            FindObjectOfType<StationManager>().ShowOutro();
        }
    }

    private IEnumerator Answered(bool answeredHow, bool startNewComparison = false)
    {
        yesButton.GetComponent<Button>().interactable = false;
        noButton.GetComponent<Button>().interactable = false;

        if(answeredHow)
            onTrue.Invoke();
        else 
            onFalse.Invoke();
        
        yield return new WaitForSecondsRealtime(answerDelay);
        
        yesButton.GetComponent<Button>().interactable = true;
        noButton.GetComponent<Button>().interactable = true;
        
        onAnswerComplete.Invoke();
        
        if (startNewComparison)
            ComparisonStart(currentComparison);
    }
}
