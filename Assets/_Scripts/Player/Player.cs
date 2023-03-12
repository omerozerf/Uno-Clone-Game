using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using NaughtyAttributes;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    public List<Card> cards = new List<Card>();

    [SerializeField] private float handLenght;
    

    private void CardSort()
    {
        float distance = handLenght / cards.Count;
        
        for (int j = 0; j < cards.Count; j++)
        {
            cards[j].transform.localPosition = new Vector3(distance * j - (handLenght - distance) / 2f, 0, 0);
            cards[j].transform.localRotation = Quaternion.identity;
        }
    }


    protected bool IsMyTurn()
    {
        if (TurnController.CurrentTurn == this)
        {
            return true;
        }

        return false;
    }


    protected void PlayCard()
    {
        
    }
        

    private void OnEnable()
    {
        GameEvents.OnCardsDeal += CardSort;
    }

    private void OnDisable()
    {
        GameEvents.OnCardsDeal -= CardSort;
    }
}
