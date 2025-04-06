using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

internal enum Turn
{
    Player,
    Enemy
}

public class CombatManager : MonoBehaviour
{
    private static CombatManager Instance { get; set; }
    private static GameObject battleUI;
    private Turn _turn;

    public void OnAtkClicked()
    {
        GameObject miniGameUI = Instance.gameObject.transform.Find("MiniGame").gameObject;
        miniGameUI.SetActive(true);
        miniGameUI.GetComponent<Canvas>().enabled = true;
        battleUI.SetActive(false);
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
        GameStateManager.Instance.TogglePause();
        
        Instance._turn = enemyAdvantage ? Turn.Enemy : Turn.Player;

        print(enemyAdvantage ? "Enemy Advantage" : "Player Advantage");

        battleUI.SetActive(true);
        
    }
}
