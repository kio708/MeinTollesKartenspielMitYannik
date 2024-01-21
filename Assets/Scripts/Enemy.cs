using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy : Entity
{
    [Serializable]
    public struct Sprite
    {
        public UnityEngine.Sprite sprite;
        public int probibilityWeight;
    }

    private int attack;

    public event Action onDeath;

    [SerializeField] private Sprite[] sprites;

    private void Awake()
    {
        int sum = 0;
        foreach(Sprite s in sprites) { sum += s.probibilityWeight; }
        int num = UnityEngine.Random.Range(0, sum);
        sum = 0;
        foreach(Sprite s in sprites)
        {
            sum += s.probibilityWeight;
            if(num < sum)
            {
                GetComponent<SpriteRenderer>().sprite = s.sprite;
                break;
            }
        }

        RoleStats();
    }

    public override void Die()
    {
        onDeath?.Invoke();
    }

    public void RoleStats()
    {
        int die1 = RoleDie();
        int die2 = RoleDie();
        attack = Mathf.Max(die1, die2);

        die1 = RoleDie();
        die2 = RoleDie();
        int die3 = RoleDie();
        int die4 = RoleDie();

        Lives = die1 + die2 + die3 + die4;
        int smallestDie = SmallestNumber(new int[]{die1, die2, die3, die4 });
        Lives -= smallestDie;
        
    }

    private int SmallestNumber(params int[] numbers)
    {
        int smallest = int.MaxValue;
        foreach(int number in numbers)
        {
            if(number < smallest)
                smallest = number;
        }

        return smallest;
    }

    private int RoleDie()
    {
        return UnityEngine.Random.Range(1, 7);
    }

    public override void StartTurn()
    {
        StartCoroutine(Turning());
    }

    private IEnumerator Turning()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 4));

        int player = UnityEngine.Random.Range(0, GameManager.Instance.players.Count);
        GameManager.Instance.players[player].TakeDamage(attack);

        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.EndTurn();
    }
}
