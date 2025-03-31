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
        Coins.text = Stats.Health.Value.ToString();
        Bombs.text = Stats.Health.Value.ToString();
        Keys.text = Stats.Health.Value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Hp.text = Stats.Health.Value.ToString();
        Coins.text = Stats.Coins.Value.ToString();
        Bombs.text = Stats.Bombs.Value.ToString();
        Keys.text = Stats.Keys.Value.ToString();
    }
}
