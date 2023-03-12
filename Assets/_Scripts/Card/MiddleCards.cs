using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;

public class MiddleCards : MonoBehaviour
{
    public static List<Card> middleCards = new List<Card>();

    private List<Card> debugMiddleCardList; 

    public static MiddleCards Instance;

    public static Card LastCard => middleCards[^1];


    private void Awake()
    {
        debugMiddleCardList = middleCards;
    }


    private void Start()
    {
        Instance = this;
    }


    public CardColorType GetMiddleCardColorType()
    {
        return LastCard.colorType;
    }
    
}
