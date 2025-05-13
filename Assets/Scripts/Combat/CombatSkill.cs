using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatSkill : ScriptableObject
{
    [SerializeField] private int MPCost;

    public virtual void PerformSkill(CombatManager combatManager)
    {
        Player_Stats.Mana.Modify(-MPCost);
    }

    public int GetMPCost()
    {
        return MPCost;
    }
}
