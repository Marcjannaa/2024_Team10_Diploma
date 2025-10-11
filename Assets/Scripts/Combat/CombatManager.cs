using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

internal enum Turn
{
    Player,
    Enemy
}

public class CombatManager : MonoBehaviour
{
    [SerializeField] private GameObject _rewardItem;
    [SerializeField] private List<CombatSkill> _combatSkills;
    public static CombatManager Instance { get; private set; }

    private static GameObject _battleUI;
    private GameObject attackButton;
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
        _battleUI.transform.Find("PlayerActionPanel").Find("ActionPanel").gameObject.SetActive(false);
        _battleUI.transform.Find("PlayerActionPanel").Find("StatsPanel").gameObject.SetActive(false);
        _miniGamePanel.SetActive(true);
    }

    public void OnSkill1Clicked()
    {
        if (_combatSkills[0].GetMPCost() <= Player_Stats.Mana.Value)
        {
            _combatSkills[0].PerformSkill(this);
            _battleUI.transform.Find("PlayerActionPanel").Find("SkillPanel").gameObject.SetActive(false);
            _battleUI.transform.Find("PlayerActionPanel").Find("ActionPanel").gameObject.SetActive(true);

            StartCoroutine(PlayerSkillRoutine());
        }
    }

    private IEnumerator PlayerSkillRoutine()
    {
        yield return StartCoroutine(FlashEnemySpriteRed());
        _playerAttacked = true;
        _turn = Turn.Enemy;
        SwitchBattleUIPanel();
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

        Instance.StartCoroutine(Instance.AttackRoutineAfterHit());
    }

    private IEnumerator AttackRoutineAfterHit()
    {
        yield return StartCoroutine(FlashEnemySpriteRed());

        _battleUI.transform.Find("PlayerActionPanel").gameObject.SetActive(false);
        _battleUI.transform.Find("EnemyActionPanel").gameObject.SetActive(true);

        var dodgeMiniGame = _battleUI.transform.Find("EnemyActionPanel").Find("DodgeMiniGame").gameObject;
        dodgeMiniGame.SetActive(true);
        dodgeMiniGame.GetComponent<MiniGame.DodgeMiniGame.DodgeGameManager>().ResetGame();
    }

    private IEnumerator FlashEnemySpriteRed()
    {
        Debug.Log("Highlighting enemy");
        Color originalColor = _enemySprite.color;
        _enemySprite.color = Color.red;
        Debug.Log("Color set to red");
        yield return new WaitForSecondsRealtime(1);
        _enemySprite.color = originalColor;
        Debug.Log("Color reset");
    }

    public static void OnDodgeEnded(bool win)
    {
        _battleUI.transform.Find("EnemyActionPanel").gameObject.SetActive(false);
        _battleUI.transform.Find("PlayerActionPanel").gameObject.SetActive(true);
        _battleUI.transform.Find("PlayerActionPanel").Find("ActionPanel").gameObject.SetActive(true);
        _battleUI.transform.Find("PlayerActionPanel").Find("StatsPanel").gameObject.SetActive(true);

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
            _battleUI.GetComponent<BattleUI>().SetSkillCostText(i, _combatSkills[i].GetMPCost().ToString());
        }

        GameObject skillButton = _battleUI.GetComponent<BattleUI>().GetSkillActionFirst();

        if (EventSystem.current.currentSelectedGameObject != skillButton)
        {
            EventSystem.current.SetSelectedGameObject(skillButton);
        }

        _inDifferentPanel = true;
    }

    public void OnItemClicked() {}

    public void OnGuardClicked()
    {
        _guardMultiplier = 0.3f;
        _turn = Turn.Enemy;
        _playerAttacked = true;
        SwitchBattleUIPanel();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _battleUI = Instance.transform.Find("BattleUI").gameObject;
            _miniGamePanel = _battleUI.transform.Find("PlayerActionPanel").Find("MiniGamePanel").gameObject;

            _battleUI.transform.Find("PlayerActionPanel").Find("ActionPanel").gameObject.SetActive(true);
            _battleUI.transform.Find("PlayerActionPanel").Find("StatsPanel").gameObject.SetActive(true);
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

        Instance.StartCoroutine(Transition.Instance.PlayTransition(() =>
        {
            _battleUI.SetActive(true);
            Transform battleSpriteTransform = EnemyGO.transform.Find("BattleSprite");
            Instance._enemySprite = battleSpriteTransform.GetComponent<Image>();
            Instance.SwitchBattleUIPanel();
            Instance.StartCoroutine(Instance.BattleLoop());
        }));
    }

    private void EnemyAction()
    {
        _battleUI.transform.Find("PlayerActionPanel").gameObject.SetActive(false);
        _battleUI.transform.Find("EnemyActionPanel").gameObject.SetActive(true);

        GameObject dodgeMiniGame = _battleUI.transform.Find("EnemyActionPanel").Find("DodgeMiniGame").gameObject;
        dodgeMiniGame.SetActive(true);
    }

    private void SwitchBattleUIPanel()
    {
        switch (_turn)
        {
            case Turn.Player:
                _battleUI.transform.Find("PlayerActionPanel").gameObject.SetActive(true);
                _battleUI.transform.Find("PlayerActionPanel").Find("ActionPanel").gameObject.SetActive(true);
                _battleUI.transform.Find("PlayerActionPanel").Find("StatsPanel").gameObject.SetActive(true);
                _battleUI.transform.Find("EnemyActionPanel").gameObject.SetActive(false);
                _miniGamePanel.SetActive(false);

                attackButton = _battleUI.GetComponent<BattleUI>().GetPlayerActionFirst();

                if (EventSystem.current.currentSelectedGameObject != attackButton)
                {
                    EventSystem.current.SetSelectedGameObject(attackButton);
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
        Instantiate(_rewardItem, pos, Quaternion.identity);
        _player.GetComponent<PlayerController>().inCombat = false;
        Time.timeScale = 1;
    }

    private IEnumerator BattleLoop()
    {
        _battleUI.GetComponent<BattleUI>().SetEnemySprite(_enemySprite.sprite);
        while (_battleOngoing)
        {
            _battleUI.GetComponent<BattleUI>().SetPlayerHealthText(Player_Stats.Health.Value.ToString());
            _battleUI.GetComponent<BattleUI>().SetPlayerMPText(Player_Stats.Mana.Value.ToString());
            _battleUI.GetComponent<BattleUI>().SetEnemyHealthSlider(_enemy.GetComponent<Enemy_Stats>().Health.Value);
            

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
    }

    public GameObject GetEnemy()
    {
        return _enemy;
    }
}