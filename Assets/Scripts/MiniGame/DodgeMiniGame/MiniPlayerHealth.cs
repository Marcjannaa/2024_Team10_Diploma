using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MiniPlayerHealth : MonoBehaviour
{
    [SerializeField] private DodgeManager dodgeManager;
    
    private float _health = 100f;
    
    public void DecreaseHealth(float amount)
    {
        _health -= amount;
        if (_health <= 0)
        {
            DodgeManager.Lose();
        }
    }
}
