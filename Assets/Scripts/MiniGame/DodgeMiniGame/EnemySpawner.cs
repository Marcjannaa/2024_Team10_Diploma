using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MiniGame.DodgeMiniGame;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    private static List<GameObject> Enemies;
    
    [SerializeField] private int rounds;
    [SerializeField] private int space = 10;
    [SerializeField] private float delay = 5f;
    [SerializeField] private List<GameObject> spawnPoints;
    private DodgeGameManager _dodgeGameManager;
    private GameObject enemy;
    private float _delayTimer = 0f; 
    private void Start()
    {
        var dodgeGameManager = GetComponent<DodgeGameManager>();
        enemy = dodgeGameManager.enemy;
        
        Enemies = new List<GameObject>();
        spawnPoints = new List<GameObject>();
        
        StartCoroutine(SpawnInRounds());
    }

    private IEnumerator SpawnInRounds()
    {
        for (var i = 0; i < rounds; i++)
        {
            Spawn();
        }
        
        if (_delayTimer > delay)
        {
            yield return null;
        }
        
        _delayTimer += Time.unscaledDeltaTime;
    }

    private void Spawn()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            var enemyGo = Instantiate(enemy.gameObject, spawnPoint.transform);
            Enemies.Add(enemyGo);
        }
    }

    public static ReadOnlyCollection<GameObject> GetEnemies()
    {
        return Enemies.AsReadOnly();
    }

    public static void ClearEnemies()
    {
        Enemies.Clear();
    }
}
