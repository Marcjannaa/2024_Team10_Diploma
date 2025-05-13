using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public StatType Type { get; set; }
    public float StatValue;

    private bool checkFlag;
    //private int _modifiedValue;

    private static List<Stat> _stats = new List<Stat>();

    public Stat(float statValue)
    {
        StatValue = statValue;
    }
    
    public Stat(float statValue, StatType type)
    {
        StatValue = statValue;
        Type = type;
        _stats.Add(this);
        //Debug.Log(_stats);
    }
    
    public Stat(bool statValue, StatType type)
    {
        setFlag(statValue);
        Type = type;
        _stats.Add(this);
        //Debug.Log(_stats);
    }

    public float Value
    {
        get
        {
            //Debug.Log(StatValue);
            return StatValue;
        }
    }

    public bool getFlag()
    {
        if (checkFlag)
        {
            return checkFlag;
        }
        return false;
    }
    public void setFlag(bool flag)
    {
        //Debug.Log(checkFlag);
        checkFlag = flag;
        //Debug.Log(checkFlag);
    }
    
    public void Modify(float amount)
    {
        var tmp = StatValue + amount;
        switch (Type)
        {
            case StatType.Health:
                float maxHp = _stats.Find(x => x.Type == StatType.MaxHealth).Value;
                Debug.Log(maxHp);
                Debug.Log(tmp);
                if ( tmp >= maxHp)
                {
                    tmp = StatValue;
                }
                break;
            case StatType.MaxHealth:
                var currHp =_stats.Find(x => x.Type == StatType.Health);
                currHp.StatValue += amount;
                if (Player_Stats.Health.StatValue > Player_Stats.MaxHealth.StatValue)
                {
                    Player_Stats.Health.setValue(Player_Stats.MaxHealth.StatValue);
                }
                Debug.Log(currHp.Value);
                break;
            case StatType.Mana:
                float maxMP = _stats.Find(x => x.Type == StatType.MaxMana).Value;
                Debug.Log(maxMP);
                Debug.Log(tmp);
                if ( tmp >= maxMP)
                {
                    tmp = StatValue;
                }
                break;
            case StatType.MaxMana:
                var currMP =_stats.Find(x => x.Type == StatType.Mana);
                currMP.StatValue += amount;
                if (Player_Stats.Mana.StatValue > Player_Stats.MaxMana.StatValue)
                {
                    Player_Stats.Mana.setValue(Player_Stats.MaxMana.StatValue);
                }
                Debug.Log(currMP.Value);
                break;
            default:
                if (tmp < 0)
                {
                    tmp = 0;
                }
                break;
        }
        StatValue = tmp;
    }

    public void setValue(float value)
    {
        StatValue = value;
    }

    public void ResetModifiers()
    {
        Debug.Log("Empty");
    }
}
