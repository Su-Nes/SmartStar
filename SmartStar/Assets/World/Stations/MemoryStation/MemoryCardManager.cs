using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MemoryCardManager : MonoBehaviour
{
    [SerializeField] private int shuffleCount;
    [SerializeField] private float solveDelay = 1.5f;
    [SerializeField] private GameObject[] cards;

    [SerializeField] private UnityEvent onCompletion, onNewRound;

    private List<MemoryCardScript> chosenCards = new();
    private List<Button> unsolvedCards = new();

    private void Awake()
    {
        InstantiateAndShuffleCards();
    }

    private void InstantiateAndShuffleCards()
    {
        foreach (GameObject card in cards)
        {
            for (int i = 2; i > 0; i--)
            {
                Button newButton = Instantiate(card, Vector3.zero, Quaternion.identity, transform).GetComponent<Button>();
                newButton.onClick.AddListener(delegate{ChooseCard(newButton.GetComponent<MemoryCardScript>());});
                unsolvedCards.Add(newButton);
            }
        }

        for (int i = shuffleCount; i > 0; i--)
            ShuffleChildren();
    }

    private void ShuffleChildren()
    {
        int index1 = Random.Range(0, transform.childCount);
        transform.GetChild(index1).SetAsFirstSibling();
    }
    
    private void ChooseCard(MemoryCardScript card)
    {
        if (card.IsFlipped)
            return;
        
        card.IsFlipped = true;
        
        chosenCards.Add(card);

        if (chosenCards.Count >= 2)
            StartCoroutine(CheckCards());
    }

    public void SetAllCardsLocked(bool state)
    {
        foreach (Button card in transform.GetComponentsInChildren<Button>())
            card.interactable = !state;
    }
    
    public void SetUnsolvedCardsLocked(bool state)
    {
        foreach (Button card in unsolvedCards)
            card.interactable = !state;
    }

    private IEnumerator CheckCards()
    {
        SetUnsolvedCardsLocked(true);
        bool isSolved = chosenCards[0].Keyword == chosenCards[1].Keyword;
        
        yield return new WaitForSeconds(solveDelay);
        
        if (isSolved)
        {
            foreach (MemoryCardScript card in chosenCards)
                unsolvedCards.Remove(card.GetComponent<Button>());
            print("correct cards");
            SFXManager.Instance.PlayCorrectSound();
            EventManager.Instance.InvokeOnCorrect();
        }
        else
        {
            SFXManager.Instance.PlayIncorrectSound();
            EventManager.Instance.InvokeOnIncorrect();
        }
        
        SetUnsolvedCardsLocked(false);
        onNewRound.Invoke();

        if (!isSolved)
        {
            foreach (MemoryCardScript card in chosenCards)
                card.UnflipCard();
        }
        
        chosenCards.Clear();

        if (unsolvedCards.Count <= 0)
        {
            onCompletion.Invoke();
        }
    }
}
