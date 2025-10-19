using System;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Prefabs.MiniGames
{
    public class BasicEnemy : EnemyMiniGameBehaviour
    {
        private float _resetTimer = 0f;
        private float _resetInterval = 0f;
        
        private void Start()
        {
            
            gameObject.transform.localPosition = new Vector2(
                5 + Random.Range(1, 5),
                0
                );
            _resetInterval = Random.Range(0.1f, 2f);
            _resetTimer = 0f;
            ResetPlayerPosition();
        }

        private void Update()
        {
            Move();

            _resetTimer += Time.unscaledDeltaTime;
            if (_resetTimer >= _resetInterval)
            {
                ResetPlayerPosition();
                _resetTimer = 0f;
                _resetInterval = Random.Range(0.1f, 2f); 
            }
        }
        protected override void Move()
        {
            moveDir = new Vector2(
                playerPos.x - transform.position.x < 0 ? -1 : 1,
                playerPos.y - transform.position.y < 0 ? -1 : 1
            );
            var position = new Vector2(transform.position.x, transform.position.y);
            rb.MovePosition(position + moveDir * (speed * Time.unscaledDeltaTime));
        }
        
        private void ResetPlayerPosition()
        {
            playerPos = new Vector2(
                playerTransform.position.x + Random.Range(-noise, noise),
                playerTransform.position.y + Random.Range(-noise, noise)
            );
            rb.AddForce((moveDir + new Vector2(-noise, noise)) * 5, ForceMode2D.Impulse);
        }
    }
}
