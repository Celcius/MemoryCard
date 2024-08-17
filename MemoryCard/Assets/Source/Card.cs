using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    private Animator _animator;

    private CardData _cardData;

    private bool _canClick = true;

    [SerializeField]
    private TextMeshProUGUI _text;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnMouseEnter()
    {
        if(!_canClick)
        {
            return;
        }
        _animator.SetBool("IsHovering", true);
    }

    private void OnMouseExit()
    {
        _animator.SetBool("IsHovering", false);
    }

    void OnMouseDown(){
        if(_canClick)
        {
            _animator.SetTrigger("FlipFront");
        }        
    }

    public void OnFlippedFront()
    {
        GameManager.Instance.OnFlippedCard(this);
    }

    public void AnimateSuccess()
    {
        _canClick = false;
    }

    public void AnimateFailure()
    {
        _animator.SetTrigger("FlipBack");
        _canClick = false;
    }

    public void Init(int cardId)
    {

        
        _cardData.CardID = cardId;
        _text.text = cardId.ToString();
    }

}
