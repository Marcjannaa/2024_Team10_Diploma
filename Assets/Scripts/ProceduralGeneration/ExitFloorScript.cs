using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitFloorScript : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> enemies = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                }
            }
            
            if (enemies.Count == 0)
            {
                Debug.Log("All enemies defeated, generating next floor...");
                LevelManager.Instance.GenerateNextFloor();
            }
            else
            {
                Debug.Log("Enemies still present. Can't leave yet!");
            }
        }
    }
}
