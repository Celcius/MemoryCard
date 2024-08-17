using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundController : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup _grid;

    private List<Card> _cards;

    public List<Card> _selectedCards;

    private const float CARD_WIDTH_RATIO = 4.5f / 3.5f;
    void Start()
    {
        _selectedCards = new List<Card>();
        _cards = new List<Card>(GameManager.Instance.StartGame(this));

        float totalSpacing = _grid.spacing.x * (_cards.Count-1);

        float rowCardSize = Mathf.Max(1, (Screen.width - totalSpacing) / (GameManager.Instance.Rows));
        Vector2 cellSize = new Vector2(rowCardSize,
                                       rowCardSize*  CARD_WIDTH_RATIO);
        _grid.cellSize = cellSize;
        foreach(Card c in _cards)
        {
            c.transform.SetParent(_grid.transform,true);
            c.EnableCard();
        }
        _grid.CalculateLayoutInputHorizontal();

        _grid.enabled = true;
        StartCoroutine(DisableGrid());
    }

    private IEnumerator DisableGrid()
    {
        yield return new WaitForSeconds(1);
        _grid.enabled = false; 
    }

    private void EnableCards()
    {
        foreach(Card c in _cards)
        {
            c.EnableCard();
        }
    }
    
    private void DisableCards()
    {
        foreach(Card c in _cards)
        {
            c.DisableCard();
        }
    }

    public void OnFlippingCard()
    {
        DisableCards();
    }

    public void OnFlippedCard(Card card, bool flippedFront)
    {
        EnableCards();

        if(flippedFront)
        {
            OnFlippedFront(card);
        }
    }

    public void OnRemovedCard(Card card)
    {
        _cards.Remove(card);
        if(_cards.Count == 0)
        {
            SceneManager.LoadScene(0);
        }
        EnableCards();
    }

    private void OnFlippedFront(Card card)
    {
          if(_selectedCards.Count == 0 || _selectedCards[0].SameGroup(card))
        {
            _selectedCards.Add(card);
            card.MarkFlipped();

            if(_selectedCards.Count == GameManager.Instance.SameElements)
            {
                foreach(Card c in _selectedCards)
                {
                     c.OnSuccessFlip();
                }
                _selectedCards.Clear();
                DisableCards();
            }
        }
        else
        {
            _selectedCards.Add(card);
            foreach(Card c in _selectedCards)
            {
                c.OnFailFlip();
            }
            _selectedCards.Clear();
            DisableCards();
        }
    }
}
