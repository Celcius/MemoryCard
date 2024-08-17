using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler
{
    private Animator _animator;

    private CardData _cardData;

    private bool _clickable = false;
    private bool _isRevealed = false;
    private bool _animatingLeave = false;
    private bool _hovering = false;

    public bool CanClick => _clickable && !_isRevealed && ! _animatingLeave;
    
    [SerializeField]
    private Image _cardImage;

    private RoundController _controller;


    private void Awake()
    {
        _controller = GameManager.Instance.GameController;
        _animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData data)
    {
        OnMouseEnter();
    }
    
    public void OnPointerExit(PointerEventData data)
    {
        OnMouseExit();
    }
    
    public void OnPointerDown(PointerEventData data)
    {
        OnMouseDown();
    }

    private void OnMouseEnter()
    {
        _hovering = true;
        if(!CanClick)
        {
            return;
        }
        _animator.SetBool("IsHovering", true);
    }


    private void OnMouseExit()
    {
        _hovering = false;
        _animator.SetBool("IsHovering", false);
    }

    void OnMouseDown(){
        if(CanClick)
        {
            _animator.SetTrigger("FlipFront");
            _controller.OnFlippingCard();
        }        
    }

    public void OnAnimationFlippedFront()
    {
        _controller.OnFlippedCard(this, true);
    }

    public void OnAnimationFlippedBack()
    {
        _isRevealed = false; 
        _controller.OnFlippedCard(this, false);
    }

    public void OnAnimationSuccess()
    {
        _controller.OnRemovedCard(this);
        Destroy(this.gameObject);
    }


    public void AnimateSuccess()
    {
        _clickable = false;
        _animator.SetTrigger("Success");
    }


    public void AnimateFailure()
    {
        _animator.SetTrigger("FlipBack");
        _clickable = false;
    }

    public void Init(int cardId, Sprite CardImage)
    {
        _cardData.CardID = cardId;
        _cardData.CardImage = CardImage;
        _cardImage.sprite = CardImage;
    } 

    public void DisableCard()
    {
        _animator.SetBool("IsHovering", false);
        _clickable = false;
    }

    public void EnableCard()
    {
        _clickable = true;
        if(_hovering)
        {
            OnMouseEnter();
        }
    }

    public void MarkFlipped()
    {
        _clickable = false;
        _isRevealed = true;
    }

    public bool SameGroup(Card card)
    {
        return _cardData.CardID == card._cardData.CardID;
    }

    public void OnSuccessFlip()
    {
        _clickable = false;
        AnimateSuccess();
    }

    public void OnFailFlip()
    {
        _clickable = false;
        AnimateFailure();
    }
}
