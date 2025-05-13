using UnityEngine;

[CreateAssetMenu(fileName = "BigDMG", menuName = "Combat Skills/Big DMG")]
public class BigDMG : CombatSkill
{
    public override void PerformSkill(CombatManager combatManager)
    {
        base.PerformSkill(combatManager);
        combatManager.GetEnemy().GetComponent<Enemy_Stats>().Health
            .Modify(-Player_Stats.Intelligence.Value * 8);
        
        Debug.Log("mana " + Player_Stats.Mana.Value);
        
    }
}