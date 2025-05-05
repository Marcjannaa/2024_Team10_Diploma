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
        RaycastHit hitInfo;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity)) {
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                var pos = new Vector3(hitInfo.point.x, hitInfo.point.y + 1.0f, hitInfo.point.z);
                Instantiate(items[choice], pos, Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
