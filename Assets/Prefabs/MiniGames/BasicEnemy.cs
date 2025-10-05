using System;
using UnityEngine;

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
            rb.MovePosition(position + moveDir * (speed * Time.fixedDeltaTime));
        }
        private void ResetPlayerPosition()
        {
            playerPos = new Vector2(
                playerTransform.position.x,
                playerTransform.position.y
            );
            //rb.AddForce(transform.forward * 2, ForceMode2D.Impulse);
            Invoke("ResetPlayerPosition", 2f);
        }
    }
}
