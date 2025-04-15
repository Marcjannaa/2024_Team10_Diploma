using System;
using UnityEngine;

namespace MiniGame.DodgeMiniGame
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Collider2D targetCollider;
        [SerializeField] private float gameTime = 15f;
        private Collider2D _selfCollider;
        private float _timer;
        private void Start()
        {
            _selfCollider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            _timer += Time.unscaledTime;
            if (IsCollidingWithTarget())
                Die();
            if (_timer > gameTime)
                Win();
        }

        private void Win()
        {
            OnGameEnded();
        }

        private void OnGameEnded()
        {
            CombatManager.OnDodgeEnded();
        }
        private bool IsCollidingWithTarget()
        {
            return _selfCollider.bounds.Intersects(targetCollider.bounds);
        }

        private void Die()
        {
            OnGameEnded();
        }
    }
}
