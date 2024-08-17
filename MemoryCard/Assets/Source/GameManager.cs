using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.ExceptionServices;
using System.Numerics;
using Unity.VisualScripting;


[CreateAssetMenu(fileName = "GameManager", menuName = "MemoryCard/GameManager")]
public class GameManager : ScriptableObject
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private int CardPairs = 0;

    [SerializeField]
    private Sprite[] PossibleCards;

    public int SameElements = 2;

    [SerializeField]
    public int Rows = 5;

    
    [SerializeField]
    private Card _cardPrefab;

    List<Card> _cards = new List<Card>();

    private RoundController _controller;
    public RoundController GameController
    {
        get => _controller;
    }

    private void OnEnable()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this;       
        }     
    }

    private void ClearCards()
    {
        for(int i = _cards.Count-1; i >= 0; i--)
        {
            if(_cards[i] != null)
            {
                Destroy(_cards[i]);
            }
        }

        _cards.Clear();

    }
    public Card[] StartGame(RoundController controller)
    {
        _controller = controller;
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks * UnityEngine.Random.Range(1, 999));
        ClearCards();

        // Choose Sets
        List<Sprite> chosenCards = new List<Sprite>(PossibleCards);
        chosenCards.Sort(RandomSort);
        for(int i = chosenCards.Count-1; i >= CardPairs; i--)
        {
            chosenCards.RemoveAt(i);
        }

        
        // Init Cards
        for(int i = 0; i < CardPairs && i < chosenCards.Count; i++)
        {
            for(int j = 0; j < SameElements; j++)
            {
                Card card = Instantiate<Card>(_cardPrefab);
                if(card != null)
                {
                    _cards.Add(card);
                    card.Init(i, chosenCards[i]);
                }
            }
        }

        _cards.Sort(RandomCardSort);

        return _cards.ToArray();
    }

    private static int RandomSort(Sprite x, Sprite y)
    {
        return UnityEngine.Random.Range(-1,1);
    }

    private static int RandomCardSort(Card x, Card y)
    {
        return UnityEngine.Random.Range(-1,1);
    }
}
