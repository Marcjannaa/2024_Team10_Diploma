using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.DodgeMiniGame2
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Transform chasingPoint;
        [SerializeField] private float moveSpeed;
        private Vector3 _targetPosition;
        private Vector3 _direction;
        private void Start()
        {
            _targetPosition = chasingPoint.position;
            _direction = (_targetPosition - transform.position).normalized;
        }

        private void FixedUpdate()
        {
            if (chasingPoint is null) return;
            transform.position += _direction * (moveSpeed * Time.unscaledDeltaTime);
        }
    }
}
