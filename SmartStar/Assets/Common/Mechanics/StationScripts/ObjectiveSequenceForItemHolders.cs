using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveSequenceForItemHolders : MonoBehaviour
{
    private List<ItemHolderScript> solvedHolders = new();
    [SerializeField] private int requiredSolvedHolderAmount = 3;

    [SerializeField] private UnityEvent onSolved;

    public void AddSolvedHolder(ItemHolderScript holder)
    {
        solvedHolders.Add(holder);

        if (IsSolved())
        {
            onSolved.Invoke();
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

    private bool IsSolved()
    {
        return solvedHolders.Count >= requiredSolvedHolderAmount;
    }
}
