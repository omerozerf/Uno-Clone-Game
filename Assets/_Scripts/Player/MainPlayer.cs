using System;
using UnityEngine;

public class MainPlayer :Player
    {
        private void Update()
        {
            ShowCard();
        }

        
        


        private void ShowCard()
        {
            foreach (var card in cards)
            {
                card.Open();
            }
        }
        
        
    }