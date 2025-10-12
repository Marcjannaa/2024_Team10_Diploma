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
            direction.y = 0f;

            transform.position += direction * _speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            CombatManager.InitiateCombat(true, other.gameObject,gameObject);
        }
    }
    
    public void LookAtGameobject(GameObject obj)
    {
        
        Vector3 direction = (obj.transform.position - transform.position).normalized;
        //direction.y = 0f;

       
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;

    }

    public void SwitchMovement()
    {
        _canMove = !_canMove;
    }
}
