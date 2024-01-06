using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class Player : Entity
{
    public List<Card> cards;
    public event Action onDeath;

    public override void Die()
    {
        onDeath?.Invoke();
    }

    public override IEnumerator StartTurn()
    {
        
    }

    public class Card
    {

    }
}
