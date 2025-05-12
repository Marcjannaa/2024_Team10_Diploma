using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player_Stats : MonoBehaviour
{
    [SerializeField] public int CoinVal = 0;
    [SerializeField] public int BombVal = 0;
    [SerializeField] public int KeyVal = 0;
    
    public static Stat MaxHealth = new Stat(100, StatType.MaxHealth);
    public static Stat Health = new Stat(100, StatType.Health);
    public static Stat Strength = new Stat(10, StatType.Strength);
    public static Stat Agility = new Stat(10, StatType.Agility);
    public static Stat Intelligence = new Stat(10, StatType.Intelligence);
    public static Stat Luck = new Stat(0f, StatType.Luck);
    public static Stat Coins = new Stat (0, StatType.Coin);
    public static Stat Bombs = new Stat (0, StatType.Bomb);
    public static Stat Keys = new Stat (0, StatType.Key);
    public static Stat LockPick = new Stat(false, StatType.LockPick);

    
    private void Start()
    {
        
        Coins.setValue(CoinVal);
        Bombs.setValue(BombVal);
        Keys.setValue(KeyVal);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryUI.Instance.ToggleUI();
        }
    }
    
}

