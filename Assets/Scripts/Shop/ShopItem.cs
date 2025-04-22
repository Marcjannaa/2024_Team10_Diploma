using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : Item
{
    [SerializeField] private Player_Stats Stats;
    
    protected override void pickUp()
    {
        if (Stats.Coins.StatValue >= 15.0)
        {
            Stats.Coins.Modify(-15.0f);
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
