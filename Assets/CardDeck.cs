using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    private List<Card> deck = new List<Card>();

    private void Awake()
    {
        for (int suit = 0; suit <= 3; suit++)
        {
            for (int value = 1; value <= 13;  value++)
            {
                deck.Add(new Card((Card.Suit)suit, (Card.Value)value));

            }
        }
    }
}
