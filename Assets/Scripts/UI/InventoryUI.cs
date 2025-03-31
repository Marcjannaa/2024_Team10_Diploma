using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Strength;
    [SerializeField] private TextMeshProUGUI Agility;
    [SerializeField] private TextMeshProUGUI Intelligence;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Player_Stats Stats;
    void Start()
    {
        canvas.gameObject.SetActive(false);
        // Strength.text = "Strength: " + Stats.Strength.Value;
        // Agility.text = "Agility: " + Stats.Agility.Value;
        // Intelligence.text = "Intelligence: " + Stats.Intelligence.Value;
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
}
