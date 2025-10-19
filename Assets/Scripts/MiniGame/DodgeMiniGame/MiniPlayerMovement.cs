using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Prefabs.MiniGames
{
    public class MiniPlayerMovement : MonoBehaviour
    {
        [SerializeField] private int speed = 50;
        private Vector2 _moveDir;
        private Rigidbody2D _rb;

        [Obsolete("Obsolete")]
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            Assert.IsNotNull(_rb, "Rigidbody is null!");
            gameObject.transform.position = Vector3.zero;

            _moveDir = new Vector2(0, 0);
         
            Physics2D.autoSimulation = false;
        }

        private void Update()
        {
            _moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (_moveDir != Vector2.zero)
            {
                _rb.MovePosition(_rb.position + _moveDir.normalized * speed * Time.unscaledDeltaTime);
            }
            
            Physics2D.Simulate(Time.unscaledDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var health = this.gameObject.GetComponent<MiniPlayerHealth>();
            Assert.IsNotNull(health, "Player's health not found!");
            if (other.gameObject.CompareTag("Enemy"))
            {
                var damage = other.gameObject.GetComponent<EnemyMiniGameBehaviour>().GetDamage();
                health.DecreaseHealth(damage);
            }
        }
    }
}
