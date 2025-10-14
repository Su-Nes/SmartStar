using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveSequenceForItemHolders : MonoBehaviour
{
    private int solvedHolders;
    [SerializeField] private int requiredSolvedHolderAmount = 3;
    [SerializeField] private float invokeDelay;

    [SerializeField] private UnityEvent onJustSolved, onTransition;

    public void AddSolvedHolder(ItemHolderScript holder)
    {
        solvedHolders++;

        if (IsSolved())
        {
            onJustSolved.Invoke();
            Invoke(nameof(InvokeOnTransition), invokeDelay);
        }
    }

    public void RemoveSolvedHolder(ItemHolderScript holder)
    {
        solvedHolders--;
    }

    private void InvokeOnTransition()
    {
        onTransition.Invoke();
    }

    private bool IsSolved()
    {
        return solvedHolders >= requiredSolvedHolderAmount;
    }
}
