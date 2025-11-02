using MiniGame;
using UnityEngine;

public class AttackGameManager : MonoBehaviour
{
    [SerializeField] private GameObject attackGameObject;

    private void OnEnable()
    {
        attackGameObject.SetActive(true);
    }

    private void OnDisable()
    {
        attackGameObject.SetActive(false);
    }
    
    public void EndGame(Player.HitResult hitResult)
    {
        attackGameObject.SetActive(false);
        CombatManager.OnAttackEnded(hitResult);
    }
}
