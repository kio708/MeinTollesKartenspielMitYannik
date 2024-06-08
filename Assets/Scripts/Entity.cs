using System.Collections;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    private int maxLives;
    [SerializeField]
    private Team team;

    public int Lives { get; protected set; }

    public void TakeDamage(int damage)
    {
        Lives -= damage;

        Debug.Log(name + $" has taken {damage} damge");

        if (Lives <= 0)
            Die();
    }

    public void HealLives(int lives)
    {
        Lives += lives;
        Lives = Mathf.Clamp(Lives, 0, maxLives);
    }

    public abstract void Die();
    public abstract void StartTurn();
}
