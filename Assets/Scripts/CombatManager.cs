using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

enum Turn
{
    Player,
    Enemy
}

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }
    private static GameObject battleUI;
    private Turn _turn;

    public void OnAtkClicked()
    {
        print("guwno");
    }
    
    public void OnSkillClicked()
    {
        print("guwno2");
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            battleUI = Instance.gameObject.transform.Find("BattleUI").gameObject;      
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

        battleUI.SetActive(true);
        
    }
}
