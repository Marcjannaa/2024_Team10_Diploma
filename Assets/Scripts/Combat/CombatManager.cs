using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Quaternion = System.Numerics.Quaternion;

internal enum Turn
{
    Player,
    Enemy
}

public class CombatManager : MonoBehaviour
{
    [SerializeField] private GameObject _rewardItem;
    [SerializeField] private List<CombatSkill> _combatSkills;
    [SerializeField] private GameObject dodgeMiniGameObject; 
    public static CombatManager Instance { get; private set; }
    private static GameObject _battleUI;
    private  GameObject  attackButton;
    private static GameObject _player;
    private static GameObject _enemy;
    private bool _enemyFirstStrike = false;
    private bool _battleOngoing;
    private float _guardMultiplier = 1;
    private Image _enemySprite;
    private static GameObject _miniGamePanel;
    private Turn _turn;
    private bool _playerAttacked = false;
    private bool _enemyHasActed = false;
    private bool _inDifferentPanel = false;

    

    void Update()
    {
        //print(_turn);
        if (Keyboard.current.backspaceKey.wasPressedThisFrame && _inDifferentPanel)
        {
            _battleUI.transform.Find("PlayerActionPanel").Find("ActionPanel").gameObject.SetActive(true);
            _battleUI.transform.Find("PlayerActionPanel").Find("SkillPanel").gameObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(attackButton);
            _inDifferentPanel = false;
        }
    }

    public void OnAtkClicked()
    {
        _battleUI.gameObject.transform.Find("PlayerActionPanel").gameObject.transform.Find("ActionPanel").gameObject.SetActive(false);
        _battleUI.transform.Find("PlayerActionPanel").gameObject.transform.Find("StatsPanel").gameObject.SetActive(false);
        _miniGamePanel.SetActive(true);
    }

    public void OnSkill1Clicked()
    {
        if (_combatSkills[0].GetMPCost() <= Player_Stats.Mana.Value)
        {
            Debug.Log("skill1");
            _combatSkills[0].PerformSkill(this);

            _battleUI.transform.Find("PlayerActionPanel").Find("SkillPanel").gameObject.SetActive(false);
            _battleUI.transform.Find("PlayerActionPanel").Find("ActionPanel").gameObject.SetActive(true);


            _playerAttacked = true;
            _turn = Turn.Enemy;
            SwitchBattleUIPanel();
        }

    }
    public static void OnAttackEnded(TimingMiniGame.HitResult hitResult)
    {
        switch (hitResult)
        {
            case TimingMiniGame.HitResult.PerfectHit:
                _enemy.GetComponent<Enemy_Stats>().Health.Modify(-Player_Stats.Strength.Value * 4);
                break;
            case TimingMiniGame.HitResult.MediumHit:
                _enemy.GetComponent<Enemy_Stats>().Health.Modify(Player_Stats.Strength.Value * 3);
                break;
            case TimingMiniGame.HitResult.NoHit:
                _enemy.GetComponent<Enemy_Stats>().Health.Modify(-Player_Stats.Strength.Value * 2);
                break;
        }
        /*
        _battleUI.transform.Find("EnemyActionPanel").gameObject.SetActive(false);
        _battleUI.transform.Find("PlayerActionPanel").gameObject.SetActive(true);
        
        Instance._enemyHasActed = true;
        Instance._playerAttacked = false;
        */
        _battleUI.transform.Find("PlayerActionPanel").gameObject.SetActive(false);
        _battleUI.transform.Find("EnemyActionPanel").gameObject.SetActive(true);

        //var dodgeMiniGame = _battleUI.transform.Find("EnemyActionPanel").Find("DodgeMiniGame").gameObject;
        //dodgeMiniGame.SetActive(true);
        //dodgeMiniGame.GetComponent<MiniGame.DodgeMiniGame.DodgeGameManager>().ResetGame();

    }

    public static void OnDodgeEnded(bool win)
    {
        _battleUI.transform.Find("EnemyActionPanel").gameObject.SetActive(false);
        _battleUI.transform.Find("PlayerActionPanel").gameObject.SetActive(true);
        _battleUI.transform.Find("PlayerActionPanel").gameObject.transform.Find("ActionPanel").gameObject.SetActive(true);
        _battleUI.transform.Find("PlayerActionPanel").gameObject.transform.Find("StatsPanel").gameObject.SetActive(true);

        if (!win)
        {
            Player_Stats.Health.Modify(-(int)_enemy.GetComponent<Enemy_Stats>().Strength.Value * Instance._guardMultiplier);
        }

        Instance._playerAttacked = false;
        Instance._turn = Turn.Player;
        Instance._guardMultiplier = 1;
        Instance.SwitchBattleUIPanel();
    }


    public void OnSkillClicked()
    {
        _battleUI.transform.Find("PlayerActionPanel").Find("ActionPanel").gameObject.SetActive(false);
        _battleUI.transform.Find("PlayerActionPanel").Find("SkillPanel").gameObject.SetActive(true);
        for (int i = 0; i < _combatSkills.Count; i++)
        {
            _battleUI.GetComponent<BattleUI>().SetSkillCostText(i,_combatSkills[i].GetMPCost().ToString());
        }
        GameObject skillButton = _battleUI.GetComponent<BattleUI>().GetSkillActionFirst();

        if (EventSystem.current.currentSelectedGameObject != skillButton)
        {
            EventSystem.current.SetSelectedGameObject(skillButton);
            Debug.Log("Skill button focus set");
        }

        _inDifferentPanel = true;
    }

    public void OnItemClicked()
    {

    }

    public void OnGuardClicked()
    {
        _guardMultiplier = 0.3f;
        _turn = Turn.Enemy;
        SwitchBattleUIPanel();
        _playerAttacked = true;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _battleUI = Instance.gameObject.transform.Find("BattleUI").gameObject;
            _miniGamePanel = _battleUI.gameObject.transform.Find("PlayerActionPanel")
                .transform.Find("MiniGamePanel").gameObject;
            _battleUI.transform.Find("PlayerActionPanel").Find("ActionPanel").gameObject.SetActive(true);
            _battleUI.transform.Find("PlayerActionPanel").gameObject.transform.Find("StatsPanel").gameObject.SetActive(true);
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

        _player.GetComponent<PlayerController>().inCombat = true;

        Instance._turn = enemyAdvantage ? Turn.Enemy : Turn.Player;
        Instance._enemyFirstStrike = enemyAdvantage;

        //Debug.Log(enemyAdvantage ? "Enemy Advantage" : "Player Advantage");

        _battleUI.SetActive(true);
        Transform battleSpriteTransform = EnemyGO.transform.Find("BattleSprite");
        Instance._enemySprite = battleSpriteTransform.GetComponent<Image>();


        Instance.SwitchBattleUIPanel();

        Instance.StartCoroutine(Instance.BattleLoop());
    }

    private void EnemyAction()
    {
        _battleUI.transform.Find("PlayerActionPanel").gameObject.SetActive(false);
        
        _battleUI.transform.Find("EnemyActionPanel").gameObject.SetActive(true);
        dodgeMiniGameObject.SetActive(true);
        var dodgeMiniGamePanel = _battleUI.transform.Find("EnemyActionPanel").Find("DodgeMiniGame").gameObject;
        dodgeMiniGamePanel.SetActive(true);
        
        //dodgeMiniGame.GetComponent<MiniGame.DodgeMiniGame.Player>().StartMiniGame();


        // _turn = Turn.Player
    }



    private void SwitchBattleUIPanel()
    {
        switch (_turn)
        {
            case Turn.Player:
                _battleUI.transform.Find("PlayerActionPanel").gameObject.SetActive(true);
                _battleUI.transform.Find("PlayerActionPanel").Find("ActionPanel").gameObject.SetActive(true);
                _battleUI.transform.Find("PlayerActionPanel").gameObject.transform.Find("StatsPanel").gameObject.SetActive(true);
                _battleUI.transform.Find("EnemyActionPanel").gameObject.SetActive(false);
                _miniGamePanel.SetActive(false);

                attackButton = _battleUI.GetComponent<BattleUI>().GetPlayerActionFirst();

                if (EventSystem.current.currentSelectedGameObject != attackButton)
                {
                    EventSystem.current.SetSelectedGameObject(attackButton);
                    Debug.Log("Attack button focus set");
                }
                break;

            case Turn.Enemy:
                _battleUI.transform.Find("PlayerActionPanel").gameObject.SetActive(false);
                _battleUI.transform.Find("EnemyActionPanel").gameObject.SetActive(true);
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
        _player.GetComponent<PlayerController>().RemoveEnemyFromList(_enemy);
        Vector3 pos = _enemy.transform.position;
        Destroy(_enemy);
        _battleOngoing = false;
        _battleUI.SetActive(false);
        Instantiate(_rewardItem, pos, UnityEngine.Quaternion.identity);
        _player.GetComponent<PlayerController>().inCombat = false;

        Time.timeScale = 1;
    }

    private IEnumerator BattleLoop()
    {
        while (_battleOngoing)
        {

            _battleUI.GetComponent<BattleUI>().SetPlayerHealthText(Player_Stats.Health.Value.ToString());
            _battleUI.GetComponent<BattleUI>().SetPlayerMPText(Player_Stats.Mana.Value.ToString());
            _battleUI.GetComponent<BattleUI>().SetEnemyHealthSlider(_enemy.GetComponent<Enemy_Stats>().Health.Value);
            _battleUI.GetComponent<BattleUI>().SetEnemySprite(_enemySprite.sprite);

            if (Player_Stats.Health.Value <= 0)
            {
                GameOver();
                yield break;
            }

            if (_enemy.GetComponent<Enemy_Stats>().Health.Value <= 0)
            {
                EnemyDefeated();
                yield break;
            }

            if (_turn == Turn.Enemy && (_playerAttacked || _enemyFirstStrike))
            {
                _enemyFirstStrike = false;
                EnemyAction();
                _playerAttacked = false;
            }

            yield return null;
        }

        Time.timeScale = 1;

        //_player.SetActive(true);
    }

    public GameObject GetEnemy()
    {
        return _enemy;
    }
    
}