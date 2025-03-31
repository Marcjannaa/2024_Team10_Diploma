using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    [SerializeField] private Player_Stats stats;
    [SerializeField] private int id;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Collect()
    {
        switch (id)
        {
            case 1:
                stats.Coins.Modify(1);
                break;
            case 2:
                stats.Bombs.Modify(1);
                break;
            case 3:
                stats.Keys.Modify(1);
                break;
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.tag);
        if (other.collider.tag.Equals("Player"))
        {
            Collect();
            Destroy(gameObject);
        }
    }
}
