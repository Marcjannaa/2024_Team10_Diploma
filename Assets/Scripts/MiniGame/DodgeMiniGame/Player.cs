using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace MiniGame.DodgeMiniGame
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Collider2D targetCollider;
        private Collider2D _selfCollider;
        public Vector2 startPos;
        private void Start()
        {
            startPos = new Vector2(transform.position.x, transform.position.y);
            _selfCollider = GetComponent<Collider2D>();
        }
        
        private void Update()
        {
            if (IsCollidingWithTarget())
                Die();
        }


        private bool IsCollidingWithTarget()
        {
            return _selfCollider.bounds.Intersects(targetCollider.bounds);
        }

        private void Die()
        {
            transform.GetComponentInParent<DodgeGameManager>().OnGameEnded(false);
        }
        
    }
}
