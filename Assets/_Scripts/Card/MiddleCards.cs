using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;

public class MiddleCards : MonoBehaviour
{
    public static List<Card> middleCards = new List<Card>();

    public static MiddleCards Instance;

    public static Card LastCard => middleCards[^1];

    private void Start()
    {
        Instance = this;
    }
    
    
}
