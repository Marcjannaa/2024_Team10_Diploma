using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;


public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI Strength;
    [SerializeField] private TextMeshProUGUI Agility;
    [SerializeField] private TextMeshProUGUI Intelligence;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Player_Stats Stats;
    [SerializeField] private RectTransform InventoryPanel;

    private List<Item> items = Inventory.Items;
    private List<GameObject> buttons = new List<GameObject>();

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
            // tmp.AddComponent<ToolTip>();
            buttons.Add(tmp);
            
            tmp.transform.SetParent(InventoryPanel);
        }
        canvas.gameObject.SetActive(false);

        
    }

    public void ToggleUI()
    {
        Debug.Log(1);
        canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
    }
    void Update()
    {
        Strength.text = "STR " +  Player_Stats.Strength.Value;
        Agility.text = "AGL " + Player_Stats.Agility.Value;
        Intelligence.text = "INT " + Player_Stats.Intelligence.Value;
    }

    public void updateInv()
    {
        int tmp = 0;
        foreach (var item in items)
        {
            var img = buttons[tmp].GetComponent<UnityEngine.UI.Image>();
            img.sprite = item.image;
                img.color = Color.white; 
            tmp++;
        }
    }
}
