using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public enum SlotType { Helmet, Armor, WeaponPrimary, WeaponSecondary }
    public SlotType slotType;
    private Image image;
    private Item equipped;
    private static List<EquipSlot> equipSlots = new List<EquipSlot>();
    

    private void Awake()
    {
        image = GetComponent<Image>();
        equipSlots.Add(this);
        gameObject.GetComponent<Button>().onClick.AddListener(delegate
        {
            Debug.Log("startujemy");
            if (equipped != null)
                unEquipItem(equipped);
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = new Color(1f, 1f, 1f, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }


    public static void updateSlot()
    {
        
        foreach(EquipSlot slot in equipSlots)
        {
            if (slot.slotType == SlotType.Helmet)
            {
                if (!Inventory.equippedHead)
                {
                    
                    slot.image.sprite = null;
                    slot.image.color = new Color(1f, 1f, 1f, 0.5f);
                }
            
            }

            if (slot.slotType == SlotType.Armor)
            {
                if (!Inventory.equippedTorso)
                {
                    
                    slot.image.sprite = null;
                    slot.image.color = new Color(1f, 1f, 1f, 0.5f);
                }
            }

            if (slot.slotType == SlotType.WeaponPrimary)
            {
                if (!Inventory.equippedPrimary)
                {
                    
                    slot.image.sprite = null;
                    slot.image.color = new Color(1f, 1f, 1f, 0.5f);
                }
            }
        
            if (slot.slotType == SlotType.WeaponSecondary)
            {
                if (!Inventory.equippedSecondary)
                {
                    
                    slot.image.sprite = null;
                    slot.image.color = new Color(1f, 1f, 1f, 0.5f);
                }
            }
        }
        
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        var draggable = eventData.pointerDrag.GetComponent<DraggableItem>();
        if (draggable != null)
        {
            if (CanEquip(draggable.item))
            {
                EquipItem(draggable.item);
            }
            else
            {
                Debug.Log("Item doesn't fit this slot");
            }
        }
    }

    private bool CanEquip(Item item)
    {
        if (item.slot == Slot.Head && slotType == SlotType.Helmet && equipped == null)
        {
            return true;
        }

        if (item.slot == Slot.Torso && slotType == SlotType.Armor && equipped == null)
        {
            return true;
        }

        if (item.slot == Slot.Primary && slotType == SlotType.WeaponPrimary && equipped == null)
        {
            return true;
        }
        
        if (item.slot == Slot.Secondary && slotType == SlotType.WeaponSecondary && equipped == null)
        {
            return true;
        }

        return false;
    }

    private void EquipItem(Item item)
    { 
        InventoryUI.Instance.remove(item);
        Inventory.equipItem(item);
//        Debug.Log("Equipping " + item.name);
        equipped = item;
        Debug.Log("Equipped " + item);
        
        image.sprite = item.image;
        image.color = Color.white;
//        Debug.Log($"Equipped {item.name} in {slotType}");
    }

    private void unEquipItem(Item item)
    {
        if (Inventory.getItems().Count < 9)
        {
            equipped = null;
            WeaponSprite.Instance.changeSprite(null);
            Inventory.unEquipItem(item);
            InventoryUI.Instance.updateInv();
            updateSlot();
        }
        else
        {
            Debug.Log("Inventory is full");
        }
        
    }
}

