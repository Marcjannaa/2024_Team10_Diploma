using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MiniPlayerHealth : MonoBehaviour
{
    [SerializeField] private DodgeManager dodgeManager;
    private float health = 100f;
    
    public void DecreaseHealth(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            DodgeManager.Lose();
        }
    }
}
