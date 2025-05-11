using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    

    public static List<Item> Items = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        Items.Clear();
        Debug.Log(Items);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void addItem(Item item)
    {
        if (Items.Count < 9)
        {
            if (item.allowLockPick)
            {
                Player_Stats.LockPick.setFlag(true);
            }
            else
            {
                Debug.Log(item);
                Player_Stats.MaxHealth.Modify(item.MaxHp);
                Player_Stats.Health.Modify(item.MaxHp);
                Player_Stats.Strength.Modify(item.STR);
                Player_Stats.Agility.Modify(item.AGL);
                Player_Stats.Intelligence.Modify(item.INT);
            }
            Items.Add(item);
        }
        
        
        
        Debug.Log(Items);
    }
    
    private List<Item> getItems()
    {
        return Items;
    }
}
