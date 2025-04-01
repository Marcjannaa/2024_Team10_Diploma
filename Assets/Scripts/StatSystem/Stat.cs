using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public StatType Type { get; set; }
    public int StatValue;
    //private int _modifiedValue;

    //private static List<Stat> _stats = new List<Stat>();

    public Stat(int statValue)
    {
        StatValue = statValue;
    }
    
    public Stat(int statValue, StatType type)
    {
        StatValue = statValue;
        Type = type;
        // _stats.Add(this);
        // Debug.Log(_stats);
    }

    public int Value
    { 
        get
        {
            return StatValue;
        }
        
    }
    public void Modify(int amount)
    {
        //var tmp = StatValue + amount;
        // switch (Type)
        // {
        //     case StatType.Health:
        //         int maxHp = _stats.Find(x => x.Type == StatType.MaxHealth).Value;
        //         if ( tmp > maxHp)
        //         {
        //             tmp = StatValue;
        //         }
        //         break;
        //     case StatType.MaxHealth:
        //         var currHp =_stats.Find(x => x.Type == StatType.Health);
        //         currHp.StatValue += amount;
        //         Debug.Log(currHp.Value);
        //         break;
        // }
        StatValue += amount;
    }

    public void ResetModifiers()
    {
        Debug.Log("Empty");
    }
}
