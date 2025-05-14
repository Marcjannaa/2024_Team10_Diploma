using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.DodgeMiniGame2
{
    public class EnemySpawner : MonoBehaviour
    {
        private float _miniGameTime;
        [SerializeField] private GameObject enemy;
        [SerializeField] private int rounds = 5;
        [SerializeField] private List<Transform> spawners;

        private void OnEnable()
        {
            _miniGameTime = GetComponentInParent<DodgeMiniGameManager>().GetMiniGameTime();
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            var interval = _miniGameTime / rounds;

            for (var i = 0; i < rounds; i++)
            {
                SpawnEnemiesAtAllPoints();
                yield return new WaitForSecondsRealtime(interval);
            }
        }

        private void SpawnEnemiesAtAllPoints()
        {
            foreach (var spawnPoint in spawners)
            {
                var enemyClone = Instantiate(enemy, spawnPoint.position, Quaternion.identity);
                enemyClone.transform.SetParent(this.gameObject.transform);
            }
        }
    }
}