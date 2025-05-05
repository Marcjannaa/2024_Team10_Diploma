using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItem : Item
{
    [SerializeField] private Player_Stats Stats;
    [SerializeField] private float price;
    [SerializeField] private TextMeshPro pricetag;

    void Start()
    {
        if (!Stats)
        {
            Stats = FindObjectOfType<Player_Stats>();
            
        }
        pricetag.text = price + "$";
    }
    protected override void pickUp()
    {
        if (Stats.Coins.StatValue >= price)
        {
            Stats.Coins.Modify(-price);
            Inventory.addItem(this);
            InventoryUI.Instance.updateInv();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Insufficient Funds");
        }
    }
}
