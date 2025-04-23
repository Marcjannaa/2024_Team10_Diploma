using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class TimingMiniGame : MonoBehaviour
{
    public enum HitResult { PerfectHit, MediumHit, NoHit }

    private bool _isTouchingTarget;

    [SerializeField] private float speed;
    [SerializeField] private Collider2D targetCollider;
    [SerializeField] private RectTransform panelBounds;
    [SerializeField] private float targetRadius = 0.5f; 
    private HitResult _hitResult;
    private Vector2 _startPos;
    private Collider2D _selfCollider;

    private void Start()
    {
        _startPos = transform.position;
        _selfCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {

        transform.Translate(Vector2.right * (speed * Time.unscaledDeltaTime));

        _isTouchingTarget = IsCollidingWithTarget();

        if (!IsInsidePanel())
        {
            EndGame(HitResult.NoHit); 
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isTouchingTarget)
            {
                transform.position = _startPos;
                EndGame(HitResult.PerfectHit);
            }
            else
            {
                EndGame(HitResult.NoHit);
            }
        }
    }

    private bool IsCollidingWithTarget()
    {
        return _selfCollider.bounds.Intersects(targetCollider.bounds);
    }

    private bool IsInsidePanel()
    {
        Vector2 localPos = transform.localPosition;
    
        return panelBounds.rect.Contains(localPos);
    }



    private void EndGame(HitResult result)
    {
        _hitResult = result;
        switch (result)
        {
            case HitResult.PerfectHit:
                Debug.Log("perfect");
                break;
            case HitResult.MediumHit:
                Debug.Log("mid");
                break;
            case HitResult.NoHit:
                Debug.Log("brak testosteronu");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(result), result, null);
        }
        CombatManager.OnAttackEnded(result);
        transform.position = _startPos;
        
    }

    public HitResult GetHit()
    {
        return _hitResult;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetRadius);
    }
}