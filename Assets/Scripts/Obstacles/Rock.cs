using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rock : MonoBehaviour
{
    [SerializeField] private List<Item> items;
    
    [Range(0f, 100f)] [SerializeField] private float itemChance = 1f;

    private void OnDestroy()
    {
        Debug.Log("OH NOES I WAWA'D MY LAST WAWA");
        float roll = Random.Range(0f, 100f);
        float cumulative = 0f;
        roll += Player_Stats.Luck.Value / 100;
        RaycastHit hitInfo;
        Ray ray = new Ray(transform.position, Vector3.down);
        cumulative += itemChance;
        if (roll <= cumulative)
        {
            int choice = Random.Range(0, items.Count);
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity)) {
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    var pos = new Vector3(hitInfo.point.x, hitInfo.point.y + 1.0f, hitInfo.point.z);
                    Instantiate(items[choice], pos, Quaternion.identity);
                }
            }
        }
    }
}
