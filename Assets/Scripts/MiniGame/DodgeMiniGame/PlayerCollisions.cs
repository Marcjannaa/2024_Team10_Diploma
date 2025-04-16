using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace MiniGame.DodgeMiniGame
{
    public class PlayerCollisions : MonoBehaviour
    {
        [SerializeField] private Collider2D targetCollider;
        private Collider2D _selfCollider;
        private DodgeGameManager _dodgeGameManager;
        private void Start()
        {
            _dodgeGameManager = transform.GetComponentInParent<DodgeGameManager>();
            _selfCollider = GetComponent<Collider2D>();
        }
        
        private void Update()
        {
            if (IsCollidingWithTarget()) Die();
        }


        private bool IsCollidingWithTarget()
        {
            return _selfCollider.bounds.Intersects(targetCollider.bounds);
        }

        private void Die()
        {
            _dodgeGameManager.OnGameEnded(false);
        }
        
    }
}
