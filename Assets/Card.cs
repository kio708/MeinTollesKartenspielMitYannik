using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public enum Suit
    {
        None,
        Spade,
        Heart,
        Club,
        Diamond
    }

    public enum Value
    {
        None,
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

    public Suit suit;
    public Value value;

    public Card(Suit suit, Value value)
    {
        this.suit = suit;
        this.value = value;
    }
}
