using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _skillsCosts;
    [SerializeField] private TMP_Text _playerHealthText;
    [SerializeField] private TMP_Text _playerManaText;
    [SerializeField] private Slider _enemyHealthSlider;
    [SerializeField] private Image _enemySprite;
    [SerializeField] private GameObject _playerActionFirst;
    [SerializeField] private GameObject _skillActionFirst;
    
    
    

    public void SetPlayerHealthText(string text)
    {
        _playerHealthText.text = "HP: " + text;
    }

    public void SetEnemyHealthSlider(float health)
    {
        _enemyHealthSlider.value = (float)(health * 0.01);
    }

    public void SetEnemySprite(Sprite sprite)
    {
        if (_enemySprite != null && sprite != null)
        {
            _enemySprite.sprite = sprite;
        }
        else
        {
            Debug.LogError("Failed to set enemy sprite: _enemySprite or sprite is null!");
        }
    }
    
    public GameObject GetPlayerActionFirst() => _playerActionFirst;
    public GameObject GetSkillActionFirst() => _skillActionFirst;

    public void SetPlayerMPText(string text)
    {
        _playerManaText.text = "MP: " + text;
    }

    public void SetSkillCostText(int skillPos, string text)
    {
        _skillsCosts[skillPos].text = "MP COST: " + text;
    }
}
