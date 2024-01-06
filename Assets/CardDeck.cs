using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject cardDeckParent;
    
    private List<GameObject> deck = new List<GameObject>();
    private List<GameObject> discard = new List<GameObject>();

    private void Awake()
    {
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
    }

    private void Start()
    {
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
    /// Pull the first card from the deck
    /// </summary>
    public void PullCard(Vector3 spawnPosition)
    {
    }

    /// <summary>
    /// Deletes a Card from the deck
    /// </summary>
    /// <param name="card"></param>
    public void RemoveCard(GameObject card)
    {
        deck.Remove(card);
        Destroy(card);
    }

    /// <summary>
    /// Deletes a Card from the deck
    /// </summary>
    /// <param name="index"></param>
    public void RemoveCard(int index)
    {
        GameObject card = deck[index];
        deck.RemoveAt(index);
        Destroy(card);
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
        for (int i = 0; i < deck.Count; i++) 
        {
            GameObject card = deck[i];
            deck.RemoveAt(i);
            deck.Insert(Random.Range(0, deck.Count - 1), card);
        }
        foreach (GameObject card in deck)
        {
            Debug.Log(card.GetComponent<Card>().suit + " " + card.GetComponent<Card>().value);
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
        discard = new List<GameObject>();
    }
}
