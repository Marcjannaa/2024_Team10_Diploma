using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPlayerHealth : MonoBehaviour
{
    [SerializeField] private MiniGameMgr miniGameMgr;
    private float health = 100f;
    
    public void DecreaseHealth(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            MiniGameMgr.Lose();
        }
    }
}
