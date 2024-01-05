using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public enum Suit
    {
        Spade,
        Heart,
        Club,
        Diamond
    }

    public enum Value
    {
        Ace = 1,
        One = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    public readonly Suit cardSuit;
    public readonly Value value;

    public Card(Suit suit, Value value)
    {
        cardSuit = suit;
        this.value = value;
    }
}
