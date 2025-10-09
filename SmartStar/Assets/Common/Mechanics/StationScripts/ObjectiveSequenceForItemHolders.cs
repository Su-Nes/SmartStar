using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveSequenceForItemHolders : MonoBehaviour
{
    private List<ItemHolderScript> solvedHolders = new();
    [SerializeField] private int requiredSolvedHolderAmount = 3;
    [SerializeField] private float invokeDelay;

    [SerializeField] private UnityEvent onJustSolved, onTransition;

    public void AddSolvedHolder(ItemHolderScript holder)
    {
        solvedHolders.Add(holder);

        if (IsSolved())
        {
            onJustSolved.Invoke();
            Invoke(nameof(InvokeOnTransition), invokeDelay);
        }
    }

    public void RemoveSolvedHolder(ItemHolderScript holder)
    {
        solvedHolders.Remove(holder);
    }

    public void ClearHolderList()
    {
        solvedHolders.Clear();
    }

    private void InvokeOnTransition()
    {
        onTransition.Invoke();
    }

    private bool IsSolved()
    {
        return solvedHolders.Count >= requiredSolvedHolderAmount;
    }
}
