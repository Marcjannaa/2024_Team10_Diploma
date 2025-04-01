using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed = 5.0f;
    private bool _canCombat = false;
    private GameObject _enemyInRange = null;
    
    void Update ()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            var direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            
            transform.Translate(direction * _speed * Time.deltaTime);
            
            if (_canCombat && Input.GetKeyDown(KeyCode.Return))
            {
                CombatManager.InitiateCombat(false,gameObject,_enemyInRange);
            }
        }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _canCombat = true;
            _enemyInRange = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _canCombat = false;
            _enemyInRange = null;
        }
    }
}
