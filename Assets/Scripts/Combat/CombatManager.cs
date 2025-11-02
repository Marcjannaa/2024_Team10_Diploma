using System.Collections;
using System.Collections.Generic;
using MiniGame;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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

    private static ArrayList enemiesInCombat = new ArrayList();
    private static GameObject _battleUI;
    private GameObject attackButton;
    private static GameObject enemyButton;
    private bool _selectingEnemyForSkill = false;

    private static GameObject _player;
    private static GameObject _enemy;
    private static GameObject _selectedEnemy;
    private static Transform _enemyList;
    private bool _enemyFirstStrike = false;
    private bool _battleOngoing;
    private float _guardMultiplier = 1;
    private static GameObject _miniGamePanel;
    private Turn _turn;
    private bool _playerAttacked = false;
    private bool _enemyHasActed = false;
    private bool _inDifferentPanel = false;

    private Coroutine _enemySelectionRoutine;

    private void Update()
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

        EventSystem.current.SetSelectedGameObject(enemyButton.gameObject);
        if (_selectedEnemy != null && !_selectedEnemy.Equals(null))
        {
            _selectedEnemy.GetComponent<EnemyUI_Interaction>().getUIComponent()
                .transform.Find("TargetSprite").gameObject.SetActive(true);
        }

        _enemySelectionRoutine = StartCoroutine(CheckFocusedButton());
    }

    private IEnumerator CheckFocusedButton()
    {
        while (true)
        {
            if (_selectedEnemy == null || _selectedEnemy.Equals(null))
                yield break;

            var focused = EventSystem.current.currentSelectedGameObject;
            if (focused == null)
            {
                yield return null;
                continue;
            }

            _selectedEnemy = focused.gameObject.GetComponent<EnemyFocusButton>().getMyEnemy();
            if (_selectedEnemy == null || _selectedEnemy.Equals(null))
                yield break;

            _selectedEnemy.GetComponent<EnemyUI_Interaction>().getUIComponent()
                .transform.Find("TargetSprite").gameObject.SetActive(true);

            foreach (Transform e in _enemyList)
            {
                if (e != _selectedEnemy.transform)
                {
                    e.GetComponent<EnemyUI_Interaction>().getUIComponent()
                        .transform.Find("TargetSprite").gameObject.SetActive(false);
                }
            }

            yield return null;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                yield break;
            }
        }
    }

    public void OnEnemyClicked()
    {
        if (_enemySelectionRoutine != null)
        {
            StopCoroutine(_enemySelectionRoutine);
            _enemySelectionRoutine = null;
        }

        foreach (Transform e in _enemyList)
        {
            e.GetComponent<EnemyUI_Interaction>().getUIComponent()
                .transform.Find("TargetSprite").gameObject.SetActive(false);
        }

        _miniGamePanel.SetActive(true);
    }

    public void OnSkill1Clicked()
    {
        if (_combatSkills[0].GetMPCost() <= Player_Stats.Mana.Value)
        {
            SelectEnemyForSkill(_combatSkills[0]);
        }
    }

    public void SelectEnemyForSkill(CombatSkill skill)
    {
        _selectingEnemyForSkill = true;

        _battleUI.transform.Find("PlayerActionPanel").Find("ActionPanel").gameObject.SetActive(false);
        _battleUI.transform.Find("PlayerActionPanel").Find("StatsPanel").gameObject.SetActive(false);
        _battleUI.transform.Find("PlayerActionPanel").Find("SkillPanel").gameObject.SetActive(false);

        EventSystem.current.SetSelectedGameObject(enemyButton.gameObject);
        if (_selectedEnemy != null && !_selectedEnemy.Equals(null))
        {
            _selectedEnemy.GetComponent<EnemyUI_Interaction>().getUIComponent()
                .transform.Find("TargetSprite").gameObject.SetActive(true);
        }

        _enemySelectionRoutine = StartCoroutine(CheckFocusedButtonForSkill(skill));
    }

    private IEnumerator CheckFocusedButtonForSkill(CombatSkill skill)
    {
        while (true)
        {
            if (_selectedEnemy == null || _selectedEnemy.Equals(null))
                yield break;

            var focused = EventSystem.current.currentSelectedGameObject;
            if (focused == null)
            {
                yield return null;
                continue;
            }

            _selectedEnemy = focused.gameObject.GetComponent<EnemyFocusButton>().getMyEnemy();
            if (_selectedEnemy == null || _selectedEnemy.Equals(null))
                yield break;

            _selectedEnemy.GetComponent<EnemyUI_Interaction>().getUIComponent()
                .transform.Find("TargetSprite").gameObject.SetActive(true);

            foreach (Transform e in _enemyList)
            {
                if (e != _selectedEnemy.transform)
                {
                    e.GetComponent<EnemyUI_Interaction>().getUIComponent()
                        .transform.Find("TargetSprite").gameObject.SetActive(false);
                }
            }

            yield return null;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                skill.PerformSkill(this);

                foreach (Transform e in _enemyList)
                {
                    e.GetComponent<EnemyUI_Interaction>().getUIComponent()
                        .transform.Find("TargetSprite").gameObject.SetActive(false);
                }

                _battleUI.transform.Find("PlayerActionPanel").Find("SkillPanel").gameObject.SetActive(false);
                _battleUI.transform.Find("PlayerActionPanel").Find("ActionPanel").gameObject.SetActive(true);
                _inDifferentPanel = false;
                _selectingEnemyForSkill = false;
                yield break;
            }
        }
    }

    private IEnumerator PlayerSkillRoutine()
    {
        yield return StartCoroutine(FlashEnemySpriteRed());
        _playerAttacked = true;
        _turn = Turn.Enemy;
        SwitchBattleUIPanel();
    }

    public static void OnAttackEnded(Player.HitResult hitResult)
    {
        float dmg = hitResult switch
        {
            Player.HitResult.PerfectHit => Player_Stats.Strength.Value * 4,
            Player.HitResult.MediumHit => Player_Stats.Strength.Value * 3,
            _ => Player_Stats.Strength.Value * 2
        };

        var enemyStats = _selectedEnemy.GetComponent<Enemy_Stats>();
        enemyStats.Health.Modify(-dmg);

        if (enemyStats.Health.Value <= 0)
        {
            Transform enemyTransform = _selectedEnemy.transform;

            if (Instance._enemySelectionRoutine != null)
            {
                Instance.StopCoroutine(Instance._enemySelectionRoutine);
                Instance._enemySelectionRoutine = null;
            }

            _battleUI.GetComponent<BattleUI>().RemoveEnemyFromList(enemyTransform);

            enemyTransform.SetParent(null);
            Destroy(_selectedEnemy);
            _selectedEnemy = null;

            if (_enemyList.childCount > 0)
            {
                _selectedEnemy = _enemyList.GetChild(0).gameObject;
                enemyButton = _selectedEnemy.GetComponent<EnemyUI_Interaction>()
                    .getUIComponent()
                    .transform.Find("EnemyFocusButton").gameObject;
                enemyButton.GetComponent<EnemyFocusButton>().setMyEnemy(_selectedEnemy);
            }
            else
            {
                Instance.EnemyDefeated();
                return;
            }
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
    }

    private IEnumerator FlashEnemySpriteRed()
    {
        yield return new WaitForSecondsRealtime(1);
    }

    public static void OnDodgeEnded(bool win)
    {
        _battleUI.transform.Find("EnemyActionPanel").gameObject.SetActive(false);
        _battleUI.transform.Find("PlayerActionPanel").gameObject.SetActive(true);
        _battleUI.transform.Find("PlayerActionPanel").Find("ActionPanel").gameObject.SetActive(true);
        _battleUI.transform.Find("PlayerActionPanel").Find("StatsPanel").gameObject.SetActive(true);

        if (!win && _selectedEnemy != null && !_selectedEnemy.Equals(null))
        {
            Player_Stats.Health.Modify(-(int)_selectedEnemy.GetComponent<Enemy_Stats>().Strength.Value * Instance._guardMultiplier);
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

        if (_selectedEnemy != null && !_selectedEnemy.Equals(null))
            _battleUI.GetComponent<BattleUI>().SetEnemyHealthSlider(_selectedEnemy.transform);

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
            Random.InitState(System.DateTime.Now.Millisecond);

            Transform presetsParent = _enemy.GetComponent<Enemy>().getPresets().transform;
            int presetCount = presetsParent.childCount;

            if (presetCount > 0)
            {
                int randomIndex = Random.Range(0, presetCount);
                _enemyList = presetsParent.GetChild(randomIndex);
            }

            Instance.SwitchBattleUIPanel();

            foreach (Transform enemy in _enemyList)
            {
                Debug.Log(enemy.name);
                _battleUI.GetComponent<BattleUI>().AddEnemyToList(enemy);
                _selectedEnemy = enemy.gameObject;
                enemyButton = enemy.GetComponent<EnemyUI_Interaction>()
                    .getUIComponent()
                    .transform.Find("EnemyFocusButton").gameObject;
                enemyButton.GetComponent<EnemyFocusButton>().setMyEnemy(enemy.gameObject);
            }
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
        while (_battleOngoing)
        {
            _battleUI.GetComponent<BattleUI>().SetPlayerHealthText(Player_Stats.Health.Value.ToString());
            _battleUI.GetComponent<BattleUI>().SetPlayerMPText(Player_Stats.Mana.Value.ToString());

            if (_selectedEnemy != null && !_selectedEnemy.Equals(null))
            {
                _battleUI.GetComponent<BattleUI>().SetEnemyHealthSlider(_selectedEnemy.transform);
            }

            if (Player_Stats.Health.Value <= 0)
            {
                GameOver();
                yield break;
            }

            if (_enemyList.childCount <= 0)
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

    public GameObject GetSelectedEnemy() => _selectedEnemy;
}
