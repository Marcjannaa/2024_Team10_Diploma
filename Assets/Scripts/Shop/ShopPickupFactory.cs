using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPickupFactory : MonoBehaviour
{
    
    [SerializeField] private List<ShopPickup> items;
    void Start()
    {
        int choice = Random.Range(0, items.Count);
        RaycastHit hitInfo;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity)) {
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                var pos = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.5f, hitInfo.point.z);
                Instantiate(items[choice], pos, Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }
}
