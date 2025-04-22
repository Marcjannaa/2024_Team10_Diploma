using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MiniGame.DodgeMiniGame;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private static List<GameObject> Enemies = new List<GameObject>();
    [SerializeField] private int rounds;
    [SerializeField] private float delay = 1f;
    [SerializeField] private List<Transform> spawnPoints;

    private DodgeGameManager _dodgeGameManager;
    private GameObject enemy;

    private void Start()
    {
        _dodgeGameManager = GetComponent<DodgeGameManager>();
        if (_dodgeGameManager == null)
        {
            Debug.LogError("DodgeGameManager not found on the same GameObject!");
            return;
        }

        enemy = _dodgeGameManager.enemy;

        if (enemy == null)
        {
            Debug.LogError("Enemy prefab not assigned in DodgeGameManager!");
            return;
        }

        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogError("Spawn points not assigned in EnemySpawner!");
            return;
        }
        StartCoroutine(SpawnInRounds());
    }

    private IEnumerator SpawnInRounds()
    {
        for (int i = 0; i < rounds; i++)
        {
            Spawn();
            yield return new WaitForSecondsRealtime(delay); 
        }
    }

    private void Spawn()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            var enemyGo = Instantiate(enemy, spawnPoint.position, Quaternion.identity);
            enemyGo.transform.SetParent(_dodgeGameManager.transform);
            enemyGo.GetComponent<EnemyMovement>().Init(_dodgeGameManager.player);
            Enemies.Add(enemyGo);
        }
    }

    public static ReadOnlyCollection<GameObject> GetEnemies()
    {
        return Enemies.AsReadOnly();
    }

    public static void ClearEnemies()
    {
        if (Enemies == null) return;
        foreach (var e in Enemies)
        {
            if (e != null)
                Destroy(e);
        }
        Enemies.Clear();
    }
    public void RestartSpawning()
    {
        StartCoroutine(SpawnInRounds());
    }

}