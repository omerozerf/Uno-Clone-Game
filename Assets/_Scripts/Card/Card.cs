using System;
using UnityEngine;

namespace _Scripts
{
    
    public class Card : MonoBehaviour
    {
        public const int NullValue = -1;
        
        [SerializeField] private SpriteRenderer front;
        [SerializeField] private SpriteRenderer back;
        
        
        public CardType type { get; private set; }
        public CardColorType colorType { get; private set; }
        public int value { get; private set; }
        public CardState cardState { get; private set; }
        

        private CardDeck deck;

        public override string ToString()
        {
            return $"{type} {colorType} {value}";
        }

        public void Setup(CardType cardType, CardColorType cardColorType, int cardValue, CardState state, CardDeck cardDeck)
        {
            type = cardType;
            colorType = cardColorType;
            value = cardValue;
            cardState = state;
            deck = cardDeck;
            
            front.sprite = deck.GetSprite(this);

            name = ToString();
        }

        public void Open()
        {
            if (cardState == CardState.Back)
            {
                front.gameObject.SetActive(true);
                back.gameObject.SetActive(false);
            }
        }

        private bool IsValid() //renk || rakam || special
        {
            var lastCard = MiddleCards.LastCard;
            
            if (lastCard.colorType == colorType)
            {
                return true;
            }

            if (lastCard.type == CardType.Number && lastCard.value == value)
            {
                return true;
            }

            if (lastCard.type == type && lastCard.type != CardType.Number)
            {
                return true;
            }

            if (type == CardType.PlusSpecial)
            {
                return true;
            }

            return false;
        }

        private void OnPlayCard()
        {
            IsValid();
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
}