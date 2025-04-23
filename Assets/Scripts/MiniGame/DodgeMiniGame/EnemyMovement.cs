using UnityEngine;

namespace MiniGame.DodgeMiniGame
{
    public class EnemyMovement : MonoBehaviour
    {
        private GameObject _player;
        private Vector2 _direction;
        [SerializeField] private float speed = 500f;

        public void Init(GameObject player)
        {
            _player = player;
            SetDirection();
        }

        private void SetDirection()
        {
            if (_player == null)
            {
                //Debug.LogError("EnemyMovement: Player is null!");
                return;
            }

            var startPosition = transform.position;
            var playerPosition = _player.transform.position;
            _direction = (playerPosition - startPosition).normalized;

            //Debug.Log($"Enemy spawned at {startPosition}, targeting {playerPosition}, direction: {_direction}");
        }

        private void Update()
        {
            if (_player == null) return;
            transform.Translate(_direction * speed * Time.unscaledDeltaTime);
            //print(gameObject.transform.position);
        }
    }
}