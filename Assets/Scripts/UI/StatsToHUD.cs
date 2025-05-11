using TMPro;
using UnityEngine;

public class StatsToHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Hp;
    [SerializeField] private TextMeshProUGUI Coins;
    [SerializeField] private TextMeshProUGUI Bombs;
    [SerializeField] private TextMeshProUGUI Keys;
    void Start()
    {
        Hp.text = Player_Stats.Health.Value.ToString();
        Coins.text = Player_Stats.Health.Value.ToString();
        Bombs.text = Player_Stats.Health.Value.ToString();
        Keys.text = Player_Stats.Health.Value.ToString();
    }
    
    void Update()
    {
        Hp.text = Player_Stats.Health.Value.ToString();
        Coins.text = Player_Stats.Coins.Value.ToString();
        Bombs.text = Player_Stats.Bombs.Value.ToString();
        Keys.text = Player_Stats.Keys.Value.ToString();
    }
}
