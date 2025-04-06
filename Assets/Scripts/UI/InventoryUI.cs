using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;


public class InventoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Strength;
    [SerializeField] private TextMeshProUGUI Agility;
    [SerializeField] private TextMeshProUGUI Intelligence;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Player_Stats Stats;
    [SerializeField] private RectTransform InventoryPanel;
    private List<Item> items = Inventory.Items;
    private List<GameObject> buttons = new List<GameObject>();

    

    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject tmp = new GameObject();
            tmp.name = i.ToString();
            tmp.AddComponent<Button>();
            tmp.AddComponent<GridLayoutGroup>();
            tmp.AddComponent<CanvasRenderer>();
            tmp.AddComponent<UnityEngine.UI.Image>();
            tmp.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
            
            buttons.Add(tmp);
            
            tmp.transform.SetParent(InventoryPanel);
        }
        canvas.gameObject.SetActive(false);

        
    }

    public void ToggleUI()
    {
        canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
    }
    void Update()
    {
        Strength.text = "Strength: " + Stats.Strength.Value;
        Agility.text = "Agility: " + Stats.Agility.Value;
        Intelligence.text = "Intelligence: " + Stats.Intelligence.Value;
    }

    public void updateInv()
    {
        Debug.Log(items.Count);
        Debug.Log(buttons.Count);
        var tmp = 0;
        foreach (var item in items)
        {
            
                Debug.Log(tmp + "Image");
                buttons[tmp].gameObject.GetComponent<UnityEngine.UI.Image>().sprite = item.image;
            

            tmp++;
        }
    }
}
