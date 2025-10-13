using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public enum SlotType { Helmet, Armor, WeaponPrimary, WeaponSecondary }
    public SlotType slotType;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = new Color(1f, 1f, 1f, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
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
        if (item.slot == Slot.Head && slotType == SlotType.Helmet)
        {
            return true;
        }

        if (item.slot == Slot.Torso && slotType == SlotType.Armor)
        {
            return true;
        }

        if (item.slot == Slot.Primary && slotType == SlotType.WeaponPrimary)
        {
            return true;
        }
        
        if (item.slot == Slot.Secondary && slotType == SlotType.WeaponSecondary)
        {
            return true;
        }

        return false;
    }

    private void EquipItem(Item item)
    {
        InventoryUI.Instance.remove(item); 
        Inventory.equipItem(item);
        image.sprite = item.image;
        image.color = Color.white;
//        Debug.Log($"Equipped {item.name} in {slotType}");
    }
}

