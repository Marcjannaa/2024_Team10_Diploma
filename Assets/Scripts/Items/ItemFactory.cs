using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemFactory : MonoBehaviour
{
    [SerializeField] private static List<Item> items = new List<Item>();
    [SerializeField] private List<Item> itemsToSpawn;
    [SerializeField] private Item defaultItem;
    private Vector3 position;
    // Start is called before the first frame update

    private void Awake()
    {
        foreach (Item i in itemsToSpawn)
        {
            items.Add(i);
        }
    }

    void Start()
    {
        
        int choice = Random.Range(0, items.Count);
        RaycastHit hitInfo;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity)) {
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground") && items.Count > 0)
            {
                var pos = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.75f, hitInfo.point.z);
                Instantiate(items[choice], pos, Quaternion.identity);
                items.RemoveAt(choice);
            }
            else
            {
                var pos = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.75f, hitInfo.point.z);
                Instantiate(defaultItem, pos, Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
