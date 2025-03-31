using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }

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
        print(enemyAdvantage ? "Enemy Advantage" : "Player Advantage");
    }
}
