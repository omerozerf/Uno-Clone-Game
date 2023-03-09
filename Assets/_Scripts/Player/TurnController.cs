using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    private static Player[] playerArray;

    public static int turn = 1;

    public static Player CurrentTurn => playerArray[turn % playerArray.Length];
    
    
    private void Start()
    {
        playerArray = FindObjectsOfType<Player>();
        
    }


    private void FixedUpdate()
    {
        Debug.Log("Turn: " + CurrentTurn.name);
    }
}
