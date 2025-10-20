using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Item equippedHead;
    public static Item equippedTorso;
    public static Item equippedFeet;
    public static Item equippedPrimary;
    public static Item equippedSecondary;
    private static List<Item> Items = new List<Item>();
    // Start is called before the first frame update
    private void Awake()
    {
        Items.Clear();
        Debug.Log(Items);
    }

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
            else if (item.equippable)
            {
                Debug.Log("Yo mama");
            }
            else
            {
                Debug.Log(item);
                Player_Stats.MaxHealth.Modify(item.MaxHp);
                Player_Stats.Strength.Modify(item.STR);
                Player_Stats.Agility.Modify(item.AGL);
                Player_Stats.Intelligence.Modify(item.INT);
                Player_Stats.Luck.Modify(item.Luck);
                Player_Stats.Coins.Modify(item.Coins);
                Player_Stats.Bombs.Modify(item.Bombs);
                Player_Stats.Keys.Modify(item.Keys);
            }
            Items.Add(item);
        }
        else
        {
            Debug.Log("Over 9 items");
        }
        foreach(Item i in Items)
        {
            Debug.Log(i);
        }
        
    }

    public static void removeItem(Item item)
    {
        Items.Remove(item);
    }
    
    public static void equipItem(Item item)
    {
        switch (item.slot)
        {
            case Slot.Head:
                if (equippedHead == null)
                    equippedHead = item;
                break;
            case Slot.Torso:
                if (equippedTorso == null)
                    equippedTorso = item;
                break;
            case Slot.Legs:
                if (equippedFeet == null)
                    equippedFeet = item;
                break;
            case Slot.Primary:
                if (equippedPrimary == null)
                    equippedPrimary = item;
                break;
            case Slot.Secondary:
                if (equippedSecondary == null)
                    equippedSecondary = item;
                break;
        }
        Player_Stats.MaxHealth.Modify(item.MaxHp);
        Player_Stats.Strength.Modify(item.STR);
        Player_Stats.Agility.Modify(item.AGL);
        Player_Stats.Intelligence.Modify(item.INT);
        Player_Stats.Luck.Modify(item.Luck);
        Items.Remove(item);
    }

    public static void unEquipItem(Item item)
    {
        switch (item.slot)
        {
            case Slot.Head:
                equippedHead = null;
                break;
            case Slot.Torso:
                equippedTorso = null;
                break;
            case Slot.Legs:
                equippedFeet = null;
                break;
            case Slot.Primary:
                equippedPrimary = null;
                break;
            case Slot.Secondary:
                equippedSecondary = null;
                break;
        }
        Player_Stats.MaxHealth.Modify(-item.MaxHp);
        Player_Stats.Strength.Modify(-item.STR);
        Player_Stats.Agility.Modify(-item.AGL);
        Player_Stats.Intelligence.Modify(-item.INT);
        Player_Stats.Luck.Modify(-item.Luck);
        Items.Add(item);
    }
    
    public static List<Item> getItems()
    {
        return Items;
    }
}
