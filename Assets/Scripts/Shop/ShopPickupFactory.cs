using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPickupFactory : MonoBehaviour
{
    
    [SerializeField] private List<ShopPickup> items;
    void Start()
    {
        int choice = Random.Range(0, items.Count);
        Instantiate(items[choice], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
