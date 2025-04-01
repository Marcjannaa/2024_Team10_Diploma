using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerHealthText;

    public void SetPlayerHealthText(string text)
    {
        _playerHealthText.text = "HP: " + text;
    }
}
