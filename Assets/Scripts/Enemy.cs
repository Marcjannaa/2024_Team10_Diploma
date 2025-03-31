using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed = 4.0f;
    private bool _canMove = true;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && _canMove)
        {
            Vector3 direction = (other.transform.position - transform.position).normalized;
            direction.y = 0;

            transform.Translate(direction * _speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            CombatManager.InitiateCombat(true);
        }
    }

    public void SwitchMovement()
    {
        _canMove = !_canMove;
    }
}
