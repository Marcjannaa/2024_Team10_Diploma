using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StatsToHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Hp;
    [SerializeField] private TextMeshProUGUI Coins;
    [SerializeField] private TextMeshProUGUI Bombs;
    [SerializeField] private TextMeshProUGUI Keys;
    [SerializeField] private Player_Stats Stats;
    void Start()
    {
        Hp.text = Stats.Health.Value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
