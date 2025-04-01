using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PickupScript : MonoBehaviour
{
    [SerializeField] private int id;
    private static Player_Stats _stats;
    
    void Start()
    {
        _stats = FindObjectOfType<Player_Stats>();
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
                _stats.Coins.Modify(1);
                break;
            case 2:
                _stats.Bombs.Modify(1);
                break;
            case 3:
                _stats.Keys.Modify(1);
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

    public static Player_Stats getStats()
    {
        return _stats;
    }
}
