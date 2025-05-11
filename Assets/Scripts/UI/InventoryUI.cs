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
            tmp.AddComponent<Button>();
            tmp.AddComponent<GridLayoutGroup>();
            tmp.AddComponent<CanvasRenderer>();
            tmp.AddComponent<UnityEngine.UI.Image>();
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
        Strength.text = "Strength: " +  Player_Stats.Strength.Value;
        Agility.text = "Agility: " + Player_Stats.Agility.Value;
        Intelligence.text = "Intelligence: " + Player_Stats.Intelligence.Value;
    }

    public void updateInv()
    {
        var tmp = 0;
        foreach (var item in items)
        {
            buttons[tmp].gameObject.GetComponent<UnityEngine.UI.Image>().sprite = item.image;
            tmp++;
        }
    }
}
