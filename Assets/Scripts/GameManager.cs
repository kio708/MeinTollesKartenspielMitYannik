using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<Enemy> enemies;
    public List<Player> players;
    public Dictionary<Entity, Coroutine> activeTurns = new();

    private Team turningTeam;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            throw new System.Exception();
        }
        Instance = this;

        turningTeam = Team.Enemy;
        ChangeActiveTeam();
    }

    public void RegisterKilledEnemy()
    {
        throw new System.NotImplementedException();
    }

    public void EndTurn(Entity entity)
    {
        activeTurns.Remove(entity);

        if (activeTurns.Count == 0)
        { ChangeActiveTeam(); }
    }

    public void ChangeActiveTeam()
    {
        if (turningTeam == Team.Player)
        {
            turningTeam = Team.Enemy;
            foreach (Enemy enemy in enemies)
                activeTurns[enemy] = StartCoroutine(enemy.StartTurn());
        }
        else if(turningTeam == Team.Enemy)
        {
            turningTeam = Team.Player;
            foreach(Player player in players)
                activeTurns[player] = StartCoroutine(player.StartTurn());
        }
    }
}
