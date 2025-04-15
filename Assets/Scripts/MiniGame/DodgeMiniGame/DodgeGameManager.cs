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
        [SerializeField] private float maxTime;
        private float _timer;
        private List<GameObject> _enemies;
        private Vector2 _panelSize;

        private void Start()
        {
            _timer = 0;
            _panelSize = gameObject.GetComponent<RectTransform>().sizeDelta;
            for (var i = 0; i < enemyCount; i++)
            {
                SpawnEnemy();
            }
        }
        public void OnGameEnded(bool win)
        {
            Debug.Log("Dodge game ended! Result: " + (win ? "Win" : "Lose"));
            _timer = 0;
            ResetGame();
            CombatManager.OnDodgeEnded(win);
        }

        public void ResetGame()
        {
            _timer = 0;
            player.transform.localPosition.Set(_panelSize.x / 2, _panelSize.y / 2, 0);
            while (_enemies.Count > 0)
            {
                foreach (var e in _enemies)
                {
                    Destroy(e);
                    _enemies.Remove(e);
                    break;
                }
            }
        }

        private void SpawnEnemy()
        {
            var spawnx = Random.Range(0, 2) == 0;
            var spawny = Random.Range(0, 2) == 0;

            var enemyGo = Instantiate(enemy, transform); // przypnij do panelu
            _enemies.Add(enemyGo);
            
            var rect = enemyGo.GetComponent<RectTransform>();

            var posX = spawnx ? _panelSize.x / 2 - 20 : -_panelSize.x / 2 + 20;
            var posY = spawny ? _panelSize.y / 2 - 20 : -_panelSize.y / 2 + 20;

            rect.anchoredPosition = new Vector2(posX, posY);
        }

        private void Update()
        {
            if (_timer >= maxTime)
            {
                OnGameEnded(true);
                return;
            }
            _timer += Time.unscaledDeltaTime;
        }
    }
}
