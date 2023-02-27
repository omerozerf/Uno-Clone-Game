using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardDealer : MonoBehaviour
    {
        [SerializeField] private Player[] players;
        [SerializeField] private int playerCardCount;

        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Deal());
                
            }
            
        }


        private IEnumerator Deal()
        {
            for (int i = 0; i < players.Length; i++)
            {
                for (int j = 0; j < playerCardCount; j++)
                {
                    yield return new WaitForSeconds(0.15f);
                    
                    int randomIndex = Random.Range(0, CardDeck.cards.Count);
                    var randomCard = CardDeck.cards[randomIndex];
                    
                    randomCard.transform.DOMove(players[i].transform.position, 0.1f);
                    players[i].cards.Add(randomCard);
                    CardDeck.cards.Remove(randomCard);
                    randomCard.transform.parent = players[i].transform;
                }
            }
            yield return new WaitForSeconds(0.2f);
            GameEvents.RaiseCardsDeal();
        }

        private void FirstCard()
        {
            int randomIndex = Random.Range(0, CardDeck.cards.Count);
            var randomCard = CardDeck.cards[randomIndex];
            
            randomCard.transform.DOMove(MiddleCards.Instance.transform.position, .5f);
            randomCard.transform.parent = MiddleCards.Instance.transform;
            randomCard.Open();
            CardDeck.cards.Remove(randomCard);
            
            MiddleCards.middleCards.Add(randomCard);
            
        }
        
        private void OnCardDeal()
        {
            FirstCard();
        }

        private void OnEnable()
        {
            GameEvents.OnCardsDeal += OnCardDeal;
        }

        private void OnDisable()
        {
            GameEvents.OnCardsDeal -= OnCardDeal;
        }
    }
