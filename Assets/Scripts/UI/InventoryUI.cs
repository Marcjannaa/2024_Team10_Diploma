using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;


public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }


    [SerializeField] private TMP_FontAsset font;
    [SerializeField] private TextMeshProUGUI Strength;
    [SerializeField] private TextMeshProUGUI Agility;
    [SerializeField] private TextMeshProUGUI Intelligence;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Player_Stats Stats;
    [SerializeField] private RectTransform InventoryPanel;
    [SerializeField] private GameObject HelmetSlot;
    [SerializeField] private GameObject ArmorSlot;
    [SerializeField] private GameObject PrimarySlot;
    [SerializeField] private GameObject SecondarySlot;

    
    private List<DraggableItem> draggableItems = new List<DraggableItem>();
    private List<Item> items = new List<Item>();
    private List<GameObject> buttons = new List<GameObject>();
    private List<ItemTooltip> tooltips = new List<ItemTooltip>();

    private int lastItemsSize;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject tmp = new GameObject();
            tmp.name = i.ToString();
            var draggable = tmp.AddComponent<DraggableItem>();
            tmp.AddComponent<GridLayoutGroup>();
            Button btn = tmp.AddComponent<Button>();
            Image img = tmp.AddComponent<Image>();
            img.color = new Color(1f, 1f, 1f, 0f);
            
            ColorBlock cb = btn.colors;
            cb.normalColor = new Color(1f, 1f, 1f, 0f); 
            cb.highlightedColor = new Color(1f, 1f, 1f, 0.1f); 
            cb.pressedColor = new Color(1f, 1f, 1f, 0.2f);    
            btn.colors = cb;
            tmp.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
            GameObject tooltipObj = new GameObject("Tooltip_" + tmp);
            tooltipObj.transform.SetParent(tmp.transform, false);
            
            
            var tooltipTMP = tooltipObj.AddComponent<TextMeshProUGUI>();
            
            tooltipTMP.fontSize = 18;
            tooltipTMP.color = Color.white;
            tooltipTMP.alignment = TextAlignmentOptions.Center;
            tooltipTMP.gameObject.SetActive(false);
            tooltipTMP.margin = new Vector4(0f, 100f, 0f, -20f);
            tooltipTMP.enableWordWrapping = false;
            tooltipTMP.font = font;
            
            
            var rect = tooltipObj.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(120f, 40f);
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.anchoredPosition = new Vector2(0, -30f);
            
            
            
            buttons.Add(tmp);
            draggableItems.Add(draggable);
            var hover = buttons[i].AddComponent<ItemTooltip>();
            
            hover.GetComponent<ItemTooltip>().tooltipText = tooltipTMP;
            tooltips.Add(hover);
            
            tmp.transform.SetParent(InventoryPanel);
        }
        canvas.gameObject.SetActive(false);
        copyList();
        addListeners();
    }

    public void ToggleUI()
    {
        copyList();
        canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
    }

    private void addListeners()
    {
        
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;

            Button btn = buttons[i].GetComponent<Button>();
            btn.onClick.AddListener(delegate
            {
                Debug.Log(index);

                if (items.Count > index && items[index] != null)
                {
                    Debug.Log("Dropping");
                    dropItem(items[index]);
                }
            });
        }
    }
    void Update()
    {
        Strength.text = "STR " +  Player_Stats.Strength.Value;
        Agility.text = "AGL " + Player_Stats.Agility.Value;
        Intelligence.text = "INT " + Player_Stats.Intelligence.Value;
    }

    public void updateInv()
    {
        copyList();
        int tmp = 0;
        foreach (var item in items)
        {
            Debug.Log(item);
            
            var img = buttons[tmp].GetComponent<UnityEngine.UI.Image>();
            
            tooltips[tmp].SetTooltip(item.effect);
            
            draggableItems[tmp].item = item;
            img.sprite = item.image;
            img.color = Color.white; 
            tmp++;
        }
    }

    private void copyList()
    {
        items.Clear();
        foreach (Item i in Inventory.getItems())
        {
            items.Add(i);
        }
    }

    private void dropItem(Item item)
    {
        remove(item);
        Inventory.removeItem(item);
        updateInv();
        
        Inventory inventory = FindObjectOfType<Inventory>();
        Debug.Log("Dropped");
        Vector3 dropPos = inventory.transform.position + new Vector3(5,0, 0);
        item.GetComponent<Transform>().position = dropPos;
    }
    
    public void remove(Item item)
    {
        var index = items.IndexOf(item);
        Debug.Log(index);
        items.Remove(item);
        copyList();
        var img = buttons[index].GetComponent<UnityEngine.UI.Image>();
        img.sprite = null;
        img.color = Color.clear;
        draggableItems[index].item = null;
        tooltips[index].SetTooltip(null);
    }
    
}
