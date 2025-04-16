using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiniGame.DodgeMiniGame
{
    public class DodgeGameManager : MonoBehaviour
    {
        [SerializeField] public GameObject player, enemy;
        [SerializeField] private float maxTime = 5f;
        private float _timer;
        private bool _gameActive = false;
        private Vector2 _panelSize;

        private void Start()
        {
            _gameActive = true;
            _timer = 0;
            _panelSize = gameObject.GetComponent<RectTransform>().transform.localScale;
        }

        public void OnGameEnded(bool win)
        {
            Debug.Log("Dodge game ended! Result: " + (win ? "Win" : "Lose"));
            _gameActive = false;
            ResetGame();
            CombatManager.OnDodgeEnded(win);
        }

        public void ResetGame()
        {
            _timer = 0;
            _gameActive = true;
            player.transform.localPosition = new Vector3(_panelSize.x / 2, _panelSize.y / 2, 0);
            foreach (var e in EnemySpawner.GetEnemies())
                Destroy(e);
            EnemySpawner.ClearEnemies();
        }
        
        public Vector2 GetPanelSize()
        {
            return _panelSize;
        }

        private void Update()
        {
            if (!_gameActive)
                return;
            
            if (_timer >= maxTime)
            {
                _gameActive = false;
                OnGameEnded(true);
                return;
            }
            _timer += Time.unscaledDeltaTime;
        }
    }
}
