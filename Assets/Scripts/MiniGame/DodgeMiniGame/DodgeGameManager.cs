using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiniGame.DodgeMiniGame
{
    public class DodgeGameManager : MonoBehaviour
    {
        [SerializeField] public GameObject player, enemy;
        [SerializeField] private int enemyCount, space = 10;
        [SerializeField] private float delay = 5f;
        [SerializeField] private float maxTime = 5f;
        private float _timer;
        private bool _gameActive = false;
        private List<GameObject> _enemies;
        private Vector2 _panelSize;

        private void Start()
        {
            _gameActive = true;
            _enemies = new List<GameObject>();
            _timer = 0;
            _panelSize = gameObject.GetComponent<RectTransform>().transform.localScale;
            for (var i = 0; i < enemyCount; i++)
                SpawnEnemy();
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
            foreach (var e in _enemies)
                Destroy(e);
            _enemies.Clear();

            for (var i = 0; i < enemyCount; i++)
                SpawnEnemy();
        }


        private void SpawnEnemy()
        {
            var enemyGo = Instantiate(enemy, transform);
            _enemies.Add(enemyGo);

            var rect = enemyGo.GetComponent<RectTransform>();

            float buffer = 100f; // jak daleko od krawędzi ma się spawnować wróg
            float posX = 0f;
            float posY = 0f;

            int side = Random.Range(0, 4); // 0 = lewo, 1 = prawo, 2 = góra, 3 = dół

            switch (side)
            {
                case 0: 
                    posX = -_panelSize.x / 2 - buffer;
                    posY = Random.Range(-_panelSize.y / 2, _panelSize.y / 2);
                    break;
                case 1: 
                    posX = _panelSize.x / 2 + buffer;
                    posY = Random.Range(-_panelSize.y / 2, _panelSize.y / 2);
                    break;
                case 2: 
                    posY = _panelSize.y / 2 + buffer;
                    posX = Random.Range(-_panelSize.x / 2, _panelSize.x / 2);
                    break;
                case 3: 
                    posY = -_panelSize.y / 2 - buffer;
                    posX = Random.Range(-_panelSize.x / 2, _panelSize.x / 2);
                    break;
            }

            rect.anchoredPosition = new Vector2(posX, posY);
        }

        public Vector2 GetPanelSize()
        {
            return _panelSize;
        }

        private void Update()
        {
            if (!_gameActive)
                return;
            print(_timer);

            if (_timer >= maxTime)
            {
                print("wtf");
                _gameActive = false;
                OnGameEnded(true);
                return;
            }
            _timer += Time.unscaledDeltaTime;
        }

    }
}
