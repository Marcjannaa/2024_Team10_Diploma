using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public StatType Type { get; set; }
    public float StatValue;
    //private int _modifiedValue;

    //private static List<Stat> _stats = new List<Stat>();

    public Stat(float statValue)
    {
        StatValue = statValue;
    }
    
    public Stat(float statValue, StatType type)
    {
        StatValue = statValue;
        Type = type;
        // _stats.Add(this);
        // Debug.Log(_stats);
    }


    public float Value
    {

        get
        {
            return StatValue;
        }
        
    }

    
    public void Modify(float amount)
    {
        var tmp = StatValue + amount;
        switch (Type)
        {
            case StatType.Health:
                float maxHp = _stats.Find(x => x.Type == StatType.MaxHealth).Value;
                if ( tmp > maxHp)
                {
                    tmp = StatValue;
                }
                break;
            case StatType.MaxHealth:
                var currHp =_stats.Find(x => x.Type == StatType.Health);
                currHp.StatValue += amount;
                Debug.Log(currHp.Value);
                break;
        }
        
        StatValue = tmp;
       
       
    }

    public void ResetModifiers()
    {
        Debug.Log("Empty");
    }
}
