using System;
using UnityEngine;

namespace Prefabs.MiniGames
{
    public class EnemyMiniGameBehaviour : MonoBehaviour
    {
        [SerializeField] protected Transform playerTransform;
        [SerializeField] protected int speed = 1;
        [SerializeField] protected int damage = 10;
        protected Rigidbody2D rb;
        [SerializeField] protected float noise = 1.5f;
        protected Vector2 moveDir;
        protected Vector2 playerPos;
        

        private void OnEnable()
        {
            rb = GetComponent<Rigidbody2D>();
            playerPos = new Vector2(
                playerTransform.position.x,
                playerTransform.position.y
            );
        }
        
        protected virtual void Move()
        {
        
        }

        public int GetDamage()
        {
            return damage;
        }
    }
}
