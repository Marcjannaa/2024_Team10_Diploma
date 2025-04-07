using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal enum Turn
{
    Player,
    Enemy
}

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }
    private static GameObject _battleUI;
    private static GameObject _player;
    private static GameObject _enemy;
    private bool _battleOngoing;
    private float _guardMultiplayer = 1;
    private Image _enemySprite;
    private static GameObject _miniGameUI;
    private Turn _turn;
    private bool _enemyHasActed = false;

    public void OnAtkClicked()
    {
        
        print(_enemy.GetComponent<Enemy_Stats>().Health.Value);

        print("atk clicked");
        //await Task.Delay(1000);

        _battleUI.SetActive(false);
        _miniGameUI.SetActive(true);

        _turn = Turn.Enemy;
        SwitchBattleUIPanel();
        
    }
    public static void OnAttackEnded(TimingMiniGame.HitResult  hitResult)
    {


        switch (hitResult)
        {
            case TimingMiniGame.HitResult.PerfectHit:
                _enemy.GetComponent<Enemy_Stats>().Health.Modify(-_player.GetComponent<Player_Stats>().Strength.Value * 4);
                break;
            case TimingMiniGame.HitResult.MediumHit:
                _enemy.GetComponent<Enemy_Stats>().Health.Modify(-_player.GetComponent<Player_Stats>().Strength.Value * 3);
                break;
            case TimingMiniGame.HitResult.NoHit:
                _enemy.GetComponent<Enemy_Stats>().Health.Modify(-_player.GetComponent<Player_Stats>().Strength.Value * 2);
                break;
        }
        
        _miniGameUI.SetActive(false);
        _battleUI.SetActive(true);
        
        Instance._turn = Turn.Player;
        Instance._guardMultiplayer = 1;
        Instance._enemyHasActed = false; 
        Instance.SwitchBattleUIPanel();
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
        //SwitchBattleUIPanel();
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _battleUI = Instance.gameObject.transform.Find("BattleUI").gameObject;      

            _miniGameUI = Instance.gameObject.transform.Find("MiniGame").gameObject;
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
    
        _player = PlayerGO;
        _enemy = EnemyGO;
        //GameStateManager.Instance.TogglePause();
        
        
    
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
        switch (_turn)
        {
            case Turn.Player:
                // Make the player action panel visible
                _battleUI.transform.Find("PlayerActionPanel").gameObject.SetActive(true);
                _miniGameUI.SetActive(false);

                // Set the focus on the attack button
                GameObject attackButton = _battleUI.GetComponent<BattleUI>().GetPlayerActionFirst();

                // Ensure focus is set to the attack button, if it's not already focused
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(attackButton);
                    Debug.Log("Attack button focus set");
                }

                break;

            case Turn.Enemy:
                // Hide the player action panel and show the mini-game UI
                _battleUI.transform.Find("PlayerActionPanel").gameObject.SetActive(false);
                _miniGameUI.SetActive(true);
                break;
        }
    }


    private void GameOver()
    {
        print("hi");
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

            if (_turn == Turn.Enemy && !_enemyHasActed) 
            {
                EnemyAction();
                _enemyHasActed = true; 
            }

            yield return null;
        }

        Time.timeScale = 1;
    }


}

