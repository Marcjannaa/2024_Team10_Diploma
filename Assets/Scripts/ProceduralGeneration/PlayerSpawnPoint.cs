using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    private void OnEnable()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.OnPlayerSpawnRequest.AddListener(SpawnPlayer);
        }
    }

    private void OnDisable()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.OnPlayerSpawnRequest.RemoveListener(SpawnPlayer);
        }
    }

    private void SpawnPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = transform.position;
            player.transform.rotation = transform.rotation;
           
            Debug.Log("Player teleported to spawn point.");
        }
        else
        {
            Debug.LogWarning("Player not found in scene!");
        }
    }
}