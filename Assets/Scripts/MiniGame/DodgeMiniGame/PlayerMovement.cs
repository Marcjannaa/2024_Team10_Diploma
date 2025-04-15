using UnityEngine;

namespace MiniGame.DodgeMiniGame
{
    public class PlayerMovement : MonoBehaviour
    {
        private Vector2 _moveDir;
        [SerializeField] private float speed = 5f;

        private void Start()
        {
            _moveDir = new Vector2();
        }

        private void Update()
        {
            _moveDir = Vector2.zero;

            if (Input.GetKey(KeyCode.W)) _moveDir.y = 1;
            if (Input.GetKey(KeyCode.S)) _moveDir.y = -1;
            if (Input.GetKey(KeyCode.D)) _moveDir.x = 1;
            if (Input.GetKey(KeyCode.A)) _moveDir.x = -1;

            Vector3 move = new Vector3(_moveDir.x, _moveDir.y, 0).normalized * (Time.unscaledDeltaTime * speed);
            transform.Translate(move);

        }
    }
}
