using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 5.0f;
    private bool _canCombat = false;
    public bool inCombat = false;
    private GameObject _enemyInRange;
    private List<GameObject> _enemiesInRange = new List<GameObject>();
    [SerializeField] private Animator anim;
    void Update ()
    {
        //print(_enemiesInRange.Count);
        if (inCombat) return;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        if (horizontal != 0 || vertical != 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
        gameObject.GetComponentInChildren<PlayerSprites>().LookLeft(Input.GetAxis("Horizontal") < 0);

        
        var direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        transform.Translate(direction * _speed * Time.deltaTime);
        //print(_canCombat);
        if (_canCombat && Input.GetKeyDown(KeyCode.Return))
        {
            CombatManager.InitiateCombat(false,gameObject,_enemyInRange);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && !_enemiesInRange.Contains(other.gameObject))
        {
            _enemiesInRange.Add(other.gameObject);
            _enemyInRange = other.gameObject;
            _canCombat = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemiesInRange.Remove(other.gameObject);
            print(_enemiesInRange.Remove(other.gameObject));

            // Przypisz nowego wroga jeśli są jeszcze inni w zasięgu
            if (_enemiesInRange.Count > 0)
            {
                _enemyInRange = _enemiesInRange[0];
                _canCombat = true;
            }
            else
            {
                _enemyInRange = null;
                _canCombat = false;
            }
        }
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        _enemiesInRange.Remove(enemy);
    }


}
