using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

enum Turn
{
    Player,
    Enemy
}

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }
    private static GameObject _battleUI;
    private GameObject _player;
    private GameObject _enemy;
    private bool _battleOngoing;
    private float _guardMultiplayer = 1;
    private Image _enemySprite;
    
    private Turn _turn;

    public async void OnAtkClicked()
    {
        _enemy.GetComponent<Enemy_Stats>().Health.Modify(-_player.GetComponent<Player_Stats>().Strength.Value * 4);

        await Task.Delay(1000);

        _turn = Turn.Enemy;
        SwitchBattleUIPanel();
    }
    
    public void OnSkillClicked()
    {
        
    }
    
    public void OnItemClicked()
    {
        
    }
    
    public void OnGuardClicked()
    {
        _guardMultiplayer = 0.3f;
        _turn = Turn.Enemy;
        SwitchBattleUIPanel();
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _battleUI = Instance.gameObject.transform.Find("BattleUI").gameObject;      
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void InitiateCombat(bool enemyAdvantage, GameObject PlayerGO, GameObject EnemyGO)
    {
        Instance._battleOngoing = true;
        Time.timeScale = 0;
    
        Instance._player = PlayerGO;
        Instance._enemy = EnemyGO;
        
        
    
        Instance._turn = enemyAdvantage ? Turn.Enemy : Turn.Player;
    
        Debug.Log(enemyAdvantage ? "Enemy Advantage" : "Player Advantage");
    
        _battleUI.SetActive(true);
        Transform battleSpriteTransform = EnemyGO.transform.Find("BattleSprite");
        Instance._enemySprite = battleSpriteTransform.GetComponent<Image>();
        
        
        Instance.SwitchBattleUIPanel();
    
        Instance.StartCoroutine(Instance.BattleLoop());
    }

    private void EnemyAction()
    {
        _player.GetComponent<Player_Stats>().Health.Modify(- _enemy.GetComponent<Enemy_Stats>().Strength.Value * _guardMultiplayer); 

    }

    private void SwitchBattleUIPanel()
    {
        switch (Instance._turn)
        {
            case Turn.Player:
                _battleUI.transform.Find("PlayerActionPanel").gameObject.SetActive(true);
                _battleUI.transform.Find("EnemyActionPanel").gameObject.SetActive(false); 
                break;
            case Turn.Enemy:
                _battleUI.transform.Find("EnemyActionPanel").gameObject.SetActive(true);  
                _battleUI.transform.Find("PlayerActionPanel").gameObject.SetActive(false);
                break;
        }
    }

    private void GameOver()
    {
        _battleUI.SetActive(false); 
        Destroy(_player);
        _battleOngoing = false;
        
        Time.timeScale = 1;
    }

    private void EnemyDefeated()
    {
        Destroy(_enemy);
        _battleOngoing = false;
        _battleUI.SetActive(false); 
        
        Time.timeScale = 1;
    }
    
    private IEnumerator BattleLoop()
    {
        while (_battleOngoing)
        {
            _battleUI.GetComponent<BattleUI>().SetPlayerHealthText(_player.GetComponent<Player_Stats>().Health.Value.ToString());
            _battleUI.GetComponent<BattleUI>().SetEnemyHealthSlider(_enemy.GetComponent<Enemy_Stats>().Health.Value);
            _battleUI.GetComponent<BattleUI>().SetEnemySprite(_enemySprite.sprite);
            
            
            if (_player.GetComponent<Player_Stats>().Health.Value <= 0)
            {
                GameOver();
                yield break;
            }

            if (_enemy.GetComponent<Enemy_Stats>().Health.Value <= 0)
            {
                EnemyDefeated();
                yield break;
            }

            if (_turn == Turn.Enemy)
            {
                EnemyAction();
                yield return new WaitForSecondsRealtime(1.5f);

                _turn = Turn.Player;
                _guardMultiplayer = 1;
                SwitchBattleUIPanel();
            }

            yield return null;
        }

        Time.timeScale = 1;
    }

}

