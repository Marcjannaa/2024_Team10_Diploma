using UnityEngine;

public class TimingMiniGame : MonoBehaviour
{
    private bool _isTouchingTarget = false;
    private bool _gameEnded = false;

    [SerializeField] private float speed;
    [SerializeField] private Collider2D targetCollider; 

    private Collider2D _selfCollider;

    private void Start()
    {
        _selfCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (_gameEnded) return;

        transform.Translate(Vector2.right * (speed * Time.unscaledDeltaTime));

        _isTouchingTarget = _selfCollider.bounds.Intersects(targetCollider.bounds);

        if (!Input.GetKeyDown(KeyCode.Space)) return;
        _gameEnded = true;

        Debug.Log(_isTouchingTarget ? "Trafione w punkt!" : "Pudło!");
    }
}