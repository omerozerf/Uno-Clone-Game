using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    private static Player[] playerArray;

    public static int Turn = 1;

    public static Player CurrentTurn => playerArray[Turn % playerArray.Length];
    
    
    private void Start()
    {
        playerArray = FindObjectsOfType<Player>();
        
    }

    private void OnPlayCard()
    {
        Debug.Log("Turn: " + CurrentTurn.name);

    }
    
    
    private void OnEnable()
    {
        GameEvents.OnCardPlay += OnPlayCard;
    }

    
    private void OnDisable()
    {
        GameEvents.OnCardPlay -= OnPlayCard;
    }
}
