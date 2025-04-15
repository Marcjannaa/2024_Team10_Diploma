using UnityEngine;

namespace MiniGame.DodgeMiniGame
{
    public class MiniGameEnemyMovement : MonoBehaviour
    {
        private GameObject _player;
        private Vector2 _playerPosition,_startPosition;
        private int _multiplierX, _multiplierY; 
        [SerializeField] private float speed = 5f;
        private void Start()
        {
            _player = transform.GetComponentInParent<DodgeGameManager>().player;
            _playerPosition = _player.transform.position;
            _startPosition = transform.position;
            _multiplierX = _playerPosition.x > _startPosition.x ? 1 : -1;
            _multiplierY = _playerPosition.y > _startPosition.y ? 1 : -1;
        }

        private void Update()
        {
            transform.Translate(
                new Vector2(
                    transform.position.x + speed * Time.unscaledDeltaTime * _multiplierX,
                    transform.position.y + speed * Time.unscaledDeltaTime * _multiplierY
                )
            );
        }
    }
}
