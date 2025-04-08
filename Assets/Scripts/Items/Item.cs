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
    [SerializeField] public Sprite image;
    [SerializeField] public InventoryUI ui;
    [SerializeField] public bool allowLockPick;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.tag);
        if (other.collider.tag.Equals("Player"))
        {
            Inventory.addItem(this);
            ui.updateInv();
            Destroy(gameObject);
        }
    }
}
