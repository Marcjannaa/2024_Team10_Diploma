using UnityEngine;

namespace MiniGame.DodgeMiniGame
{
    public class EnemyMovement : MonoBehaviour
    {
        private GameObject _player;
        private Vector2 _direction;
        private int _multiplierX, _multiplierY;
        [SerializeField] private float speed = 500f;


        private void Start()
        {
            _player = transform.GetComponentInParent<DodgeGameManager>().player;
            if (_player == null)
            {
                Debug.LogError("Player not found in DodgeGameManager!");
                return;
            }

            var startPosition = transform.position;
            var playerPosition = _player.transform.position;
            _multiplierX = startPosition.x > playerPosition.x ? -1 : 1;
            _multiplierY = startPosition.y > playerPosition.y ? -1 : 1;
            _direction = (playerPosition - startPosition).normalized;

            Debug.Log($"Enemy spawned at {startPosition}, targeting {playerPosition}, direction: {_direction}");
        }

        private void Update()
        {
            transform.Translate(_direction * speed * Time.unscaledDeltaTime);
        }


    }
}