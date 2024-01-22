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

    public static Player lastlySelectedPlayer;

    private bool isClicked = false;

    private void OnMouseDown()
    {
        isClicked = true;
    }

    private void OnMouseExit()
    {
        isClicked = false;
    }

    private void OnMouseUp()
    {
        if (isClicked && lastlySelectedPlayer == null)
        {
            lastlySelectedPlayer = this;
        }
        isClicked = false;
    }

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

        StartCoroutine(WaitForPlayerTurnEnd());
    }

    private IEnumerator WaitForPlayerTurnEnd()
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

        //Highlight chosen card here
        lastlySelectedPlayer = null;
        Enemy.lastlySelectedEnemy = null;

        if (chosenCard.suit == Card.Suit.Heart)
            while (lastlySelectedPlayer == null) yield return null;
        else
            while (Enemy.lastlySelectedEnemy == null) yield return null;


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
