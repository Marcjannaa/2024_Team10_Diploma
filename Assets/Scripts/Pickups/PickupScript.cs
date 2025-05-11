using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PickupScript : MonoBehaviour
{
    [SerializeField] protected int id;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void Collect()
    {
        switch (id)
        {
            case 1:
                Player_Stats.Coins.Modify(1);
                Destroy(gameObject);
                break;
            case 2:
                Player_Stats.Bombs.Modify(1);
                Destroy(gameObject);
                break;
            case 3:
                Player_Stats.Keys.Modify(1);
                Destroy(gameObject);
                break;
            case 4:
                var tmp = Player_Stats.Health.Value;
                Player_Stats.Health.Modify(10);
                if (tmp != Player_Stats.Health.Value)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.tag);
        if (other.collider.tag.Equals("Player"))
        {
            Collect();
        }
    }
    
}
