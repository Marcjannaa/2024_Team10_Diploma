using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private SpriteRenderer renderer;
    private static Player_Stats _stats;
    private bool playerInRange;
    void Start()
    {
        _stats = FindObjectOfType<Player_Stats>();
        renderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ExplodeBomb());
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    IEnumerator ExplodeBomb()
    {
        float delay = 0.8f;
        float delayDecrease = 0.08f;
        int flashes = 15;

        for (int i = 0; i < flashes; i++)
        {
            renderer.color = (i % 2 == 0) ? Color.white : Color.red;
            
            yield return new WaitForSeconds(delay);
            
            delay = Mathf.Max(0.05f, delay - delayDecrease);
        }

        if (playerInRange)
        {
            _stats.Health.Modify(-30);
        }
        Destroy(gameObject);
    }
}
