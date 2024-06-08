using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public static CardDeck Instance { get; private set; }

    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject cardDeckParent;
    [SerializeField] private string spritePath;

    private List<GameObject> deck = new List<GameObject>();
    private List<GameObject> discard = new List<GameObject>();

    private void Awake()
    {
        Instance = this;

        //Generates a default deck of cards.
        //A total of 52 cards is generated, 13 of each suit.
        for (int suit = 1; suit <= 4; suit++)
        {
            for (int value = 1; value <= 13;  value++)
            {
                GameObject card = cardPrefab;
                card.GetComponent<Card>().suit = (Card.Suit)suit;
                card.GetComponent<Card>().value = (Card.Value)value;
                GameObject cardObject = Instantiate(card, Vector3.zero, Quaternion.identity, cardDeckParent.GetComponent<Transform>());
                cardObject.name = card.GetComponent<Card>().value + " of " + card.GetComponent<Card>().suit;
                cardObject.SetActive(false);
                deck.Add(cardObject);
            }
        }

        Shuffle();
    }

    /// <summary>
    /// Add a Card to the Discard pile
    /// </summary>
    /// <param name="card"></param>
    public void DiscardCard(GameObject card)
    {
        discard.Add(card);
    }

    /// <summary>
    /// Pulls the first card from the deck
    /// </summary>
    /// <returns>A reference to this card. The gameobject itself is not activated yet.</returns>
    public Card PullCard()
    {
        GameObject cardGO = deck[0];
        Card card = cardGO.GetComponent<Card>();
        deck.RemoveAt(0);
        int index = (((int)card.suit - 1) * 15) + (int)card.value - 1;
        string name = spritePath + "_" + index;

        Sprite[] sprites = Resources.LoadAll<Sprite>(spritePath);
        card.GetComponent<SpriteRenderer>().sprite = sprites.Where(s => s.name == name).FirstOrDefault();

        return card;
    }

    }

    /// <summary>
    /// Add a Card to the deck
    /// </summary>
    /// <param name="card"></param>
    public void AddCard(GameObject card)
    {
        deck.Add(card);
    }


    /// <summary>
    /// Shuffle all cards in the deck
    /// </summary>
    public void Shuffle()
    {
        List<int> indices = new List<int>();
        for (int i = 0; i < deck.Count; i++)
            indices.Add(i);

        while(indices.Count > 0)
        {
            int index = indices[Random.Range(0, indices.Count)];
            indices.Remove(index);
            Debug.Log(index);

            GameObject card = deck[index];
            deck.RemoveAt(index);
            deck.Add(card);
        }
    }

    /// <summary>
    /// Move all cards from the Discard pile to the Deck
    /// </summary>
    public void MoveDiscardToDeck()
    {
        foreach (GameObject card in discard)
        {
            AddCard(card);
        }
        discard.Clear();
    }
}
