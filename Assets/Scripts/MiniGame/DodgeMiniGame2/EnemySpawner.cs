using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.DodgeMiniGame2
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float miniGameTime = 10f;
        [SerializeField] private GameObject enemy;
        [SerializeField] private int rounds = 5;
        [SerializeField] private List<Transform> spawners;

        private void Start()
        {
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            var interval = miniGameTime / rounds;

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
                Instantiate(enemy, spawnPoint.position, Quaternion.identity);
            }
        }
    }
}