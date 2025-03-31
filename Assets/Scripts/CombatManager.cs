using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

enum Turn
{
    Player,
    Enemy
}

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }
    private Turn _turn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void InitiateCombat(bool enemyAdvantage)
    {
        Time.timeScale = 0;
        
        Instance._turn = enemyAdvantage ? Turn.Enemy : Turn.Player;
        
        print(enemyAdvantage ? "Enemy Advantage" : "Player Advantage");
    }
}
