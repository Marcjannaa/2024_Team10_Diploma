using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private SpriteRenderer renderer;
    private bool playerInRange;
    private List<Collider> toDestroy = new List<Collider>();
    private BoxCollider bc;
    

    void Start()
    {
        bc = GetComponent<BoxCollider>();
        renderer = GetComponent<SpriteRenderer>();
        FindOverLap();
        StartCoroutine(ExplodeBomb());
    }
    
    void Update()
    {
        
    }

    void FindOverLap()
    {
        Vector3 worldCenter = transform.TransformPoint(bc.center);
        Vector3 halfExtents = Vector3.Scale(bc.size * 0.5f, transform.lossyScale);
        Quaternion orientation = transform.rotation;

        Collider[] hits = Physics.OverlapBox(worldCenter, halfExtents, orientation);
        
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Destroyable"))
            {
                Debug.Log("Object already inside trigger: " + hit.name);
                toDestroy.Add(hit);
            }
        }
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
            Player_Stats.Health.Modify(-30);
        }

        if (toDestroy.Count > 0)
        {
            Debug.Log(toDestroy.Count);
            foreach (var obj in toDestroy)
            {
                Debug.Log("Destroyed!");
                Destroy(obj.gameObject);
            }
        }
        Destroy(gameObject);
    }
}
