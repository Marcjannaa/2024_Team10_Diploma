using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    static Player_Stats stats;

    public static List<Item> Items = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        Items.Clear();
        stats = FindObjectOfType<Player_Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void addItem(Item item)
    {
        if (Items.Count < 9)
        {
            stats.MaxHealth.Modify(item.MaxHp);
            stats.Health.Modify(item.MaxHp);
            stats.Strength.Modify(item.STR);
            stats.Agility.Modify(item.AGL);
            stats.Intelligence.Modify(item.INT);
            Items.Add(item);
        }
        
        
        
        Debug.Log(Items);
    }
    
    private List<Item> getItems()
    {
        return Items;
    }
}
