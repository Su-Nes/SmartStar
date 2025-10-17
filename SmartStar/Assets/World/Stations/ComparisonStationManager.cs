using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ComparisonStationManager : MonoBehaviour
{
    [SerializeField] private string[] objectTitles, objectDisplayNames;
    [SerializeField] private Sprite[] objectSprites;
    private string currentComparison, otherComparison;
    [SerializeField] private DisplayString[] titleTexts, comparisonTexts;
    [SerializeField] private Image[] objectDisplayImages;
    [SerializeField] private DisplayString countdownText;
    [SerializeField] private UnityEvent onTrue, onFalse;

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
            onTrue.Invoke();
            ComparisonStart(currentComparison);
        }
        else
        {
            onFalse.Invoke();
        }
        
        EndCheck();
    }

    public void AnswerNo()
    {
        if (currentComparison.ToLower().Contains(otherComparison.ToLower()))
        {
            onTrue.Invoke();
        }
        else
        {
            onFalse.Invoke();
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
}
