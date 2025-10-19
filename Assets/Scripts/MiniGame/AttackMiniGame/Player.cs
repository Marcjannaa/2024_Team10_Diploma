using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace MiniGame
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private Collider2D perfectHitbox, mediumHitbox;
        [SerializeField] private AttackGameManager attackGameManager;
        [SerializeField] private Vector3 startPosition = new Vector3(-1, 0, 0);
        
        private Rigidbody2D _rb;
        private Collider2D _collider;
        
        public enum HitResult {PerfectHit, MediumHit, NoHit}
        
        [Obsolete("Obsolete")]
        private void OnEnable()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            gameObject.transform.position = startPosition;
            
            Physics2D.autoSimulation = false;

        }

        private void Update()
        {
            _rb.velocity = new Vector2(speed, 0);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_collider.IsTouching(perfectHitbox))
                {
                    EndGame(HitResult.PerfectHit);
                }
                else if (_collider.IsTouching(mediumHitbox))
                {
                    EndGame(HitResult.MediumHit);
                }
                else
                {
                    EndGame(HitResult.NoHit);
                }
            }
            Physics2D.Simulate(Time.unscaledDeltaTime);

        }

        private void EndGame(HitResult gameResult)
        {
            attackGameManager.EndGame(gameResult);
        }
    }
}
