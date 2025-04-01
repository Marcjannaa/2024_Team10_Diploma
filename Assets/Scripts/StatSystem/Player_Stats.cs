using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player_Stats : MonoBehaviour
{
    public Stat MaxHealth = new Stat(110, StatType.MaxHealth);
    public Stat Health = new Stat(100, StatType.Health);
    public Stat Strength = new Stat(10, StatType.Strength);
    public Stat Agility = new Stat(10, StatType.Agility);
    public Stat Intelligence = new Stat(10, StatType.Intelligence);
    public Stat Coins = new Stat (0, StatType.Coin);
    public Stat Bombs = new Stat (0, StatType.Bomb);
    public Stat Keys = new Stat (0, StatType.Key);
    [SerializeField] private InventoryUI inventoryUI;

   

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            if (inventoryUI != null)
            {
                inventoryUI.ToggleUI();
            }
    }
}

public enum StatType
{
    Health,
    MaxHealth,
    Strength,
    Agility,
    Intelligence,
    Coin,
    Bomb,
    Key
    
}