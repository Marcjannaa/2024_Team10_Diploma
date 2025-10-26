using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] public string effect;
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
    [SerializeField] public bool equippable;
    [SerializeField] public Slot slot;

    private bool canPickup = true;
    
    private void Start()
    {
        if (!equippable)
        {
            slot = Slot.None;
        }
        StartCoroutine(pickupCD());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator pickupCD()
    {
        canPickup = false;
        yield return new WaitForSeconds(2);
        canPickup = true;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.tag);
        if (other.collider.tag.Equals("Player"))
        {
            pickUp();
        }
    }
    
    void StripDownToSelf()
    {
        var components = GetComponents<Component>();

        foreach (var comp in components)
        {
            if (comp is Transform || comp == this)
                continue;

            Destroy(comp);
        }
    }
    
    protected virtual void pickUp()
    {
        if (Inventory.getItems().Count < 9 && canPickup)
        {
            
            Inventory.addItem(this);
            InventoryUI.Instance.updateInv();
            
            GetComponent<Transform>().position = new Vector3(0, -100, 0);
        }
        // else
        // {
        //     Debug.Log("Inventory Full not picking up");
        // }
        
    }
}
