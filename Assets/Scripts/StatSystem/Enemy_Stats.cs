using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats : MonoBehaviour
{ 
    public Stat MaxHealth = new Stat(100, StatType.MaxHealth);
    public Stat Health = new Stat(100, StatType.Health);
    public Stat Strength = new Stat(10, StatType.Strength);
}
