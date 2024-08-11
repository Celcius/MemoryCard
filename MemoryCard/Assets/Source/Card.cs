using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Card : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnMouseEnter()
    {
        _animator.SetBool("IsHovering", true);
    }

    private void OnMouseExit()
    {
        _animator.SetBool("IsHovering", false);
    }

    void OnMouseDown(){
        Debug.Log("Sprite Clicked");
    }
}
