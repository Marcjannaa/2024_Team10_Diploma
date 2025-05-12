using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private string effect;
    [SerializeField] public int MaxHp;
    [SerializeField] public int STR;
    [SerializeField] public int AGL;
    [SerializeField] public int INT;
    [SerializeField] public int Luck;
    [SerializeField] public int Coins;
    [SerializeField] public int Bombs;
    [SerializeField] public int Keys;
    [SerializeField] public Sprite image;
    [SerializeField] public bool allowLockPick;
    
    
   

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.tag);
        if (other.collider.tag.Equals("Player"))
        {
            pickUp();
        }
    }

    protected virtual void pickUp()
    {
        Inventory.addItem(this);
        InventoryUI.Instance.updateInv();
        Destroy(gameObject);
    }
}
