using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameManager", menuName = "MemoryCard/GameManager")]
public class GameManager : ScriptableObject
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    public int CardPairs = 0;
    
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
}
