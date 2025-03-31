using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    public Stat MaxHealth = new Stat { BaseValue = 100 };
    public Stat Health = new Stat { BaseValue = 100 };
    public Stat Strength = new Stat { BaseValue = 10 };
    public Stat Agility = new Stat { BaseValue = 10 };
    public Stat Intelligence = new Stat { BaseValue = 10 };
    public Stat Coins = new Stat { BaseValue = 0 };
    public Stat Bombs = new Stat { BaseValue = 0 };
    public Stat Keys = new Stat { BaseValue = 0 };
    
    
    
    // void Update()
    // {
    //     if (Input.GetButtonDown("Jump"))
    //     {
    //         Health.Modify(1);
    //         Coins.Modify(1);
    //         Bombs.Modify(1);
    //         Keys.Modify(1);
    //     }
    // }
}

[System.Serializable]
public class Stat
{
    public int BaseValue;
    private int _modifiedValue;

    public int Value
    {
        get { return BaseValue + _modifiedValue; }
    }

    public void Modify(int amount)
    {
        _modifiedValue += amount;
    }

    public void ResetModifiers()
    {
        _modifiedValue = 0;
    }
}