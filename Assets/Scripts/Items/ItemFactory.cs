using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    [SerializeField] private List<Item> items;
    private Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        int choice = Random.Range(0, items.Count);
        Instantiate(items[choice], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
