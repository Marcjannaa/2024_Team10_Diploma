using UnityEngine;
using UnityEngine.Assertions;

namespace Prefabs.MiniGames
{
    public class MiniPlayerMovement : MonoBehaviour
    {
        [SerializeField] private int speed = 5;
        private Vector2 moveDir;
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            Assert.IsNotNull(rb, "Rigidbody is null!");
            moveDir = new Vector2(0, 0);
        }

        private void Update()
        {
            moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        private void FixedUpdate()
        {
            var currentPos = new Vector2(transform.position.x,
                transform.position.y);
            rb.MovePosition(
                currentPos + moveDir * speed * Time.fixedUnscaledDeltaTime
            );
        }

        private void OnCollisionEnter2D(Collision2D other)
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
