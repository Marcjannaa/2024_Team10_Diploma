using UnityEngine;

namespace MiniGame.DodgeMiniGame
{
    public class MiniGamePlayerMovement : MonoBehaviour
    {
        private Vector2 _moveDir;
        [SerializeField] private float speed = 5f;

        private void Start()
        {
            _moveDir = new Vector2();
        }

        private void Update()
        {
            _moveDir.x = Input.GetAxis("Horizontal");
            _moveDir.y = Input.GetAxis("Vertical");
            
            if (_moveDir is { x: 0, y: 0 }) return;
            var move = new Vector3(_moveDir.x, _moveDir.y, 0) * (Time.unscaledDeltaTime * speed);
            transform.Translate(move);

        }
    }
}
