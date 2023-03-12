using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardDeck : MonoBehaviour
{
    public static List<Card> cards = new List<Card>();
    
    
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Sprite[] redCards;
    [SerializeField] private Sprite[] yellowCards;
    [SerializeField] private Sprite[] blueCards;
    [SerializeField] private Sprite[] greenCards;
    [SerializeField] private Sprite[] blockCards;
    [SerializeField] private Sprite[] reverseCards;
    [SerializeField] private Sprite[] plusCards;
    [SerializeField] private Sprite[] specialCards;
    
    
    
    private void Start()
    {
        CreateDeck();
    }


    public Sprite GetSprite(Card card)
    {
        if (card.type == CardType.Number)
        {
            return card.colorType switch
            {
                CardColorType.Red => redCards[card.value],
                CardColorType.Blue => blueCards[card.value],
                CardColorType.Green => greenCards[card.value],
                CardColorType.Yellow => yellowCards[card.value],
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        if (card.type == CardType.Block) 
        {
            return blockCards[(int)card.colorType];
        }

        if (card.type == CardType.Reverse)
        {
            return reverseCards[(int)card.colorType];
        }

        if (card.type == CardType.Plus)
        {
            return plusCards[(int)card.colorType];
        }

        return card.type == CardType.ColorSpecial
            ? specialCards[0]
            : specialCards[1];
    }

        
    private void CreateCard(CardType cardType, CardColorType cardColorType, int cardValue)
    {
        Card newCard = Instantiate(cardPrefab, transform);
        cards.Add(newCard);
        
        newCard.Setup(cardType, cardColorType, cardValue, CardState.Back, this);
    }
    
    private void CreatePlusCard(CardColorType cardColorType, int cardValue)
    {
        CreateCard(CardType.Plus, cardColorType, cardValue);
    }

    private void CreateBlockCard(CardColorType cardColorType)
    {
        CreateCard(CardType.Block, cardColorType, Card.NullValue);
    }

    private void CreateReverseCard(CardColorType cardColorType)
    {
        CreateCard(CardType.Reverse, cardColorType, Card.NullValue);
    }
    
    private void CreateNumberCard(CardColorType cardColorType, int cardValue)
    {
        CreateCard(CardType.Number, cardColorType, cardValue);
    }

    private void CreateColorSpecialCard()
    {
        CreateCard(CardType.ColorSpecial, CardColorType.Black, Card.NullValue);
    }

    private void CreatePlusSpecial(int cardValue)
    {
        CreateCard(CardType.PlusSpecial, CardColorType.Black, Card.NullValue);
    }

    private void CreateColorCards(CardColorType cardColorType)
    {
        CreateNumberCard(cardColorType, 0);

        for (int i = 1; i <= 9; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                CreateNumberCard(cardColorType, i);
            }
        }

        for (int i = 0; i < 2; i++)
        {
            CreateBlockCard(cardColorType);
        }

        for (int i = 0; i < 2; i++)
        {
            CreateReverseCard(cardColorType);
        }

        for (int i = 0; i < 2; i++)
        {
            CreatePlusCard(cardColorType, 2);
        }

    }

    // private void CreateSpecialCards()
    // {
    //     for (int i = 0; i < 4; i++)
    //     {
    //         CreateColorSpecialCard();
    //     }
    //     
    //     for (int i = 0; i < 4; i++)
    //     {
    //         CreatePlusSpecial(4);
    //     }
    // }

    private void CreateDeck()
    {
        CreateColorCards(CardColorType.Red);
        CreateColorCards(CardColorType.Blue);
        CreateColorCards(CardColorType.Yellow);
        CreateColorCards(CardColorType.Green);
        
        // CreateSpecialCards();
    }
    
    
    public static void TakeCard(Player player)
    {
        int randomIndex = Random.Range(0, cards.Count);
        
        var card = cards[randomIndex];

        card.Open();
        card.transform.DOMove(player.transform.position, 1f).OnComplete(() =>
        {
            cards.Remove(card);
            player.cardList.Add(card);
            GameEvents.RaiseCardPlay();
        });
    }
}
