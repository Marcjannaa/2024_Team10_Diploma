using UnityEngine;

public class Projectile : MonoBehaviour, IPoolableObject {
    [SerializeField] private float speed;
    [SerializeField] private int dmg;
    private IObjectPool _pool;
    private Rigidbody _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    public void SetPool(IObjectPool pool) {
        _pool = pool;
    }

    public void Spawn(Vector3 position, Vector3 direction) {
        transform.position = position;
        gameObject.SetActive(true);

        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.velocity = direction * speed;
    }

    public void Despawn() {
        gameObject.SetActive(false);
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Turret")) return;

        if (other.collider.CompareTag("Player")) {
            Player_Stats.Health.Modify(-dmg);
        }

        _pool?.ReleaseObject(this);
    }
}