using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRewardFactory : MonoBehaviour
{
    [SerializeField] private GameObject Coin;
    [SerializeField] private GameObject Bomb;
    [SerializeField] private GameObject Key;
    [SerializeField] private GameObject Heart;
    [SerializeField] private GameObject Chest;
    [SerializeField] private GameObject GoldenChest;

    // Szanse procentowe
    [Range(0f, 100f)] [SerializeField] private float goldenChestChance = 10f;
    [Range(0f, 100f)] [SerializeField] private float chestChance = 20f;
    [Range(0f, 100f)] [SerializeField] private float keyChance = 30f;
    [Range(0f, 100f)] [SerializeField] private float bombChance = 20f;
    [Range(0f, 100f)] [SerializeField] private float coinChance = 15f;
    [Range(0f, 100f)] [SerializeField] private float heartChance = 5f;

    void Start()
    {
        float roll = Random.Range(0f, 100f);
        roll += Player_Stats.Luck.Value / 100;
        float cumulative = 0f;
        RaycastHit hitInfo;
        Ray ray = new Ray(transform.position, Vector3.down);
        cumulative += goldenChestChance;
        if (roll <= cumulative)
        {
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity)) {
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    var pos = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
                    Instantiate(GoldenChest, pos, Quaternion.Euler(new Vector3(0, -90, 0)));
                }
            }
            Destroy(gameObject);
            return;
        }

        cumulative += chestChance;
        if (roll <= cumulative)
        {
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity)) {
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    var pos = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
                    Instantiate(Chest, pos, Quaternion.Euler(new Vector3(0, -90, 0)));
                }
            }
            Destroy(gameObject);
            return;
        }

        cumulative += keyChance;
        if (roll <= cumulative)
        {
            Instantiate(Key, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        cumulative += bombChance;
        if (roll <= cumulative)
        {
            Instantiate(Bomb, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        cumulative += coinChance;
        if (roll <= cumulative)
        {
            Instantiate(Coin, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        cumulative += heartChance;
        if (roll <= cumulative)
        {
            Instantiate(Heart, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
