using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class Player : Entity
{
    [SerializeField] private GameObject[] cardHolders;
    [SerializeField] private Collider2D discardPile;

    [HideInInspector] public List<Card> cards;
    [HideInInspector] public Card chosenCard;
    public event Action onDeath;

    public override void Die()
    {
        onDeath?.Invoke();
    }

    public override void StartTurn()
    {
        StartCoroutine(DrawCards());
    }

    private IEnumerator DrawCards()
    {
        while (cards.Count < 3)
        {
            yield return new WaitForSeconds(1);

            Card card = CardDeck.Instance.PullCard();
            card.transform.position = Vector3.zero;
            int holder = 0;
            while (cardHolders[holder].transform.childCount != 0)
                holder++;

            card.transform.SetParent(cardHolders[holder].transform, false);
            card.gameObject.SetActive(true);
            cards.Add(card);
        }

        StartCoroutine(WaitForChosenCard());
    }

    private IEnumerator WaitForChosenCard()
    {
        foreach(Card card in cards)
        {
            TouchMover mover = card.GetComponent<TouchMover>(); 
            mover.CanMove = true;
            mover.discardPile = discardPile;
            mover.player = this;
        }

        while(chosenCard == null)
        {
            yield return null;
        }

        cards.Remove(chosenCard);

        foreach (Card card in cards)
        {
            TouchMover mover = card.GetComponent<TouchMover>();
            mover.discardPile = null;
        }

        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.EndTurn();
    }
}
