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
            Debug.Log("Players finished;");
            turningTeam = Team.Enemy;
            foreach (Enemy enemy in enemies)
            {
                enemy.StartTurn();
                missingClosedTurns++;
            }
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
}
