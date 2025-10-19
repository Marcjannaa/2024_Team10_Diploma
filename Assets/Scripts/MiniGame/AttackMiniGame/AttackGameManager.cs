using System.Collections;
using System.Collections.Generic;
using MiniGame;
using UnityEngine;

public class AttackGameManager : MonoBehaviour
{
   
   public void EndGame(Player.HitResult hitResult)
   {
      CombatManager.OnAttackEnded(hitResult);
   }
}
