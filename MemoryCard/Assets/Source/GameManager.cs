using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.ExceptionServices;
using System.Numerics;


[CreateAssetMenu(fileName = "GameManager", menuName = "MemoryCard/GameManager")]
public class GameManager : ScriptableObject
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private int CardPairs = 0;
    private int SameElements = 2;

    [SerializeField]
    public int Rows = 5;

    
    [SerializeField]
    private Card _cardPrefab;

    List<Card> _cards = new List<Card>();

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
    public Card[] StartGame()
    {
        ClearCards();
        
        List<int> symbolData = new List<int>();
        for(int i = 0; i < CardPairs; i++)
        {
            for(int j = 0; j < SameElements; j++)
            {
                Card card = Instantiate<Card>(_cardPrefab);
                if(card != null)
                {
                    _cards.Add(card);
                    symbolData.Add(j);   
                }
            }
        }

        symbolData.Sort(RandomSort);
    
        for(int i = 0; i < symbolData.Count; i++)
        {
            _cards[i].Init(symbolData[i]);
        }

        return _cards.ToArray();
    }

    private static int RandomSort(int x, int y)
    {
        return UnityEngine.Random.Range(-1,1);
    }

    public void OnFlippedCard(Card card)
    {


        //card.AnimateFailure();
        /*if(Random.Range(0,5 % 2 == 0))
        {
            card.AnimateSuccess();
        }
        else
        {
            card.AnimateFailure();
        }*/
    }
}
