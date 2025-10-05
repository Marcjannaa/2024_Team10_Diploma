using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Prefabs.MiniGames
{
    public class BasicEnemy : EnemyMiniGameBehaviour
    {
        private void Awake()
        {
            Invoke("ResetPlayerPosition", 2f);
        }

        protected override void Move()
        {
            moveDir = new Vector2(
                playerPos.x - transform.position.x < 0 ? -1 : 1,
                playerPos.y - transform.position.y < 0 ? -1 : 1
            );
            var position = new Vector2(transform.position.x, transform.position.y);
            rb.MovePosition(position + moveDir * (speed * Time.fixedUnscaledDeltaTime));
        }
        
        private void ResetPlayerPosition()
        {
            playerPos = new Vector2(
                playerTransform.position.x + Random.Range(-noise, noise),
                playerTransform.position.y + Random.Range(-noise, noise)
            );
            rb.AddForce((moveDir + new Vector2(-noise, noise)) * 5 , ForceMode2D.Impulse);
            Invoke("ResetPlayerPosition", Random.Range(0.1f, 2));
        }
    }
}
