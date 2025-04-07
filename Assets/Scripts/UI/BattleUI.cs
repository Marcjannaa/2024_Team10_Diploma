using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerHealthText;
    [SerializeField] private Slider _enemyHealthSlider;
    [SerializeField] private Image _enemySprite;
    [SerializeField] private GameObject _playerActionFirst;
    
    
    

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

}
