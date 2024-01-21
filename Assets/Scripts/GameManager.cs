using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<Enemy> enemies;
    public List<Player> players;
    private int missingClosedTurns;

    private Team turningTeam;

    private void Awake()
    {
        Instance = this;

        turningTeam = Team.Enemy;
        ChangeActiveTeam();
    }

    public void RegisterKilledEnemy()
    {
        throw new System.NotImplementedException();
    }

    public void EndTurn()
    {
        missingClosedTurns--;

        if (missingClosedTurns == 0)
        { ChangeActiveTeam(); }
    }

    public void ChangeActiveTeam()
    {
        if (turningTeam == Team.Player)
        {
            StartCoroutine(EndPlayerTurn());
        }
        else if(turningTeam == Team.Enemy)
        {
            turningTeam = Team.Player;
            foreach (Player player in players)
            {
                player.StartTurn();
                missingClosedTurns++;
            }
        }
    }

    private IEnumerator EndPlayerTurn()
    {
        Card card1 = players[0].chosenCard;
        Card card2 = players[1].chosenCard;
        players[0].chosenCard = null;
        players[1].chosenCard = null;
        Effect[] effects = EvaluatePlayerCards(card1, card2);

        turningTeam = Team.Enemy;
        foreach (Enemy enemy in enemies)
        {
            enemy.StartTurn();
            missingClosedTurns++;
        }

        yield return null;
    }

    private struct Effect
    {
        public bool isHealing;
        public int value;
        public Entity entity;

        public Effect(bool isHealing, int value, Entity entity)
        {
            this.isHealing = isHealing;
            this.value = value;
            this.entity = entity;
        }
    }

    private Effect[] EvaluatePlayerCards(Card card1, Card card2)
    {
        List<Effect> effects = new List<Effect>();

        if(card1.value == card2.value)
        {
            foreach (Enemy enemy in enemies)
            {
                effects.Add(new Effect(false, (int)card1.value, enemy));
            }

            if(card1.suit == Card.Suit.Heart)
            {
                foreach(Player player in players)
                {
                    effects.Add(new Effect(true, Mathf.CeilToInt((int)card1.value * 0.5f), player));
                }
            } 
            else if (card2.suit == Card.Suit.Heart)
            {
                foreach (Player player in players)
                {
                    effects.Add(new Effect(true, Mathf.CeilToInt((int)card2.value * 0.5f), player));
                }
            }
        }

        return effects.ToArray();
    }
}
