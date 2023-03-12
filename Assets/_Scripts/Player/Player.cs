using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    public List<Card> cardList = new List<Card>();

    [SerializeField] private float handLenght;

    private bool hasTakeCard = false;

     [SerializeField] bool hasPunish = false;

    protected bool HasValidCard => cardList.Any(card => card.IsValid);


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayCard();
        }
        
        ShowCard();
    }


    private void CardSort()
    {
        float distance = handLenght / cardList.Count;
        
        for (int j = 0; j < cardList.Count; j++)
        {
            cardList[j].transform.localPosition = new Vector3(distance * j - (handLenght - distance) / 2f, 0, 0);
            cardList[j].transform.localRotation = Quaternion.identity;
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
        if (!IsMyTurn()) return;

        if (hasPunish)
        {
            hasPunish = !hasPunish;
            TakeCardFromMiddle(this);
            TakeCardFromMiddle(this);
            TurnController.Turn++;
            return;
        }
        
        if (!HasValidCard)
        {
            if (!hasTakeCard)
            {
                TakeCardFromMiddle(this);
            }
            else
            {
                TurnController.Turn++;
                hasTakeCard = false;
            }
        }
        
        
        foreach (var card in cardList)
        {
            if (card.IsValid)
            {
                if (card.type == CardType.Block)
                {
                    cardList.Remove(card);
                    card.Open();

                    card.transform.DOMove(MiddleCards.Instance.transform.position, 1f).OnComplete(() =>
                    {
                        TurnController.Turn += 2;
                        //MiddleCards.LastCard.gameObject.SetActive(false);
                        MiddleCards.middleCards.Add(card);
                        //GameEvents.RaiseCardPlay();
                        MiddleCards.middleCards[^2].gameObject.SetActive(false);
                        card.transform.parent = MiddleCards.Instance.transform;
                    });
                    card.transform.DORotate(Vector3.zero, 1f);
                    GameEvents.OnCardPlay();
                    break;
                }

                // if (card.type == CardType.Plus)
                // {
                //     cardList.Remove(card);
                //     card.Open();
                //     TurnController.Turn += 1;
                //
                //     card.transform.DOMove(MiddleCards.Instance.transform.position, 1f).OnComplete(() =>
                //     {
                //         MiddleCards.middleCards.Add(card);
                //         MiddleCards.middleCards[^2].gameObject.SetActive(false);
                //         card.transform.parent = MiddleCards.Instance.transform;
                //         TakeCardFromMiddle(TurnController.CurrentTurn);
                //     });
                //     card.transform.DORotate(Vector3.zero, 1f);
                //     GameEvents.OnCardPlay();
                //     break;
                // }
                
                if (card.type == CardType.Reverse)
                {
                    cardList.Remove(card);
                    card.Open();

                    card.transform.DOMove(MiddleCards.Instance.transform.position, 1f).OnComplete(() =>
                    {
                        TurnController.Turn--;
                        TurnController.Turn *= -1;
                        //MiddleCards.LastCard.gameObject.SetActive(false);
                        MiddleCards.middleCards.Add(card);
                        //GameEvents.RaiseCardPlay();
                        MiddleCards.middleCards[^2].gameObject.SetActive(false);
                        card.transform.parent = MiddleCards.Instance.transform;
                    });
                    card.transform.DORotate(Vector3.zero, 1f);
                    GameEvents.OnCardPlay();
                    break;
                }
                
                if (card.type == CardType.Plus)
                {
                    cardList.Remove(card);
                    card.Open();

                    card.transform.DOMove(MiddleCards.Instance.transform.position, 1f).OnComplete(() =>
                    {
                        TurnController.Turn++;
                        //MiddleCards.LastCard.gameObject.SetActive(false);
                        MiddleCards.middleCards.Add(card);
                        //GameEvents.RaiseCardPlay();
                        MiddleCards.middleCards[^2].gameObject.SetActive(false);
                        card.transform.parent = MiddleCards.Instance.transform;
                    });
                    card.transform.DORotate(Vector3.zero, 1f);
                    TurnController.playerArray
                            [Mathf.Abs(TurnController.Turn % TurnController.playerArray.Length + 1)]
                        .hasPunish = true;
                    GameEvents.OnCardPlay();
                    break;
                    
                }
                
                else
                {
                    cardList.Remove(card);
                    card.Open();

                    card.transform.DOMove(MiddleCards.Instance.transform.position, 1f).OnComplete(() =>
                    {
                        TurnController.Turn++;
                        //MiddleCards.LastCard.gameObject.SetActive(false);
                        MiddleCards.middleCards.Add(card);
                        //GameEvents.RaiseCardPlay();
                        MiddleCards.middleCards[^2].gameObject.SetActive(false);
                        card.transform.parent = MiddleCards.Instance.transform;
                    });
                    card.transform.DORotate(Vector3.zero, 1f);
                    GameEvents.OnCardPlay();
                    break;
                }
                
            }
        }
    }
    
    
    private void ShowCard()
    {
        foreach (var card in cardList)
        {
            card.Open();
        }
    }
    
    
    protected void TakeCardFromMiddle(Player player)
    {
        
        CardDeck.TakeCard(player);
        hasTakeCard = true;
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
