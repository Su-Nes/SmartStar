using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ComparisonStationManager : MonoBehaviour
{
    [SerializeField] private string[] objectNames;
    private string currentComparison, otherComparison;
    [SerializeField] private DisplayString comparisonText, countdownText;
    [SerializeField] private UnityEvent onTrue, onFalse;

    [SerializeField] private int turnCount = 10;
    [SerializeField] private bool canRepeat;
    
    
    private void Start()
    {
        ComparisonStart();
        countdownText.ShowStringRaw(turnCount.ToString());
    }

    private void ComparisonStart(string lastUsedComparison = "")
    {
        currentComparison = objectNames[Random.Range(0, objectNames.Length)];
        if(!canRepeat && lastUsedComparison == currentComparison)
            ComparisonStart(lastUsedComparison);
            
        comparisonText.ShowStringTMP(currentComparison);
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
        if (currentComparison == otherComparison)
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
        if (currentComparison != otherComparison)
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
