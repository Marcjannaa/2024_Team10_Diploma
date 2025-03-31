using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed = 4.0f;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 direction = (other.transform.position - transform.position).normalized;
            direction.y = 0;

            transform.Translate(direction * _speed * Time.deltaTime);
        }
    }

}
