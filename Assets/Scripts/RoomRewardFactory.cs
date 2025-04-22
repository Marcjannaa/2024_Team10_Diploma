using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRewardFactory : MonoBehaviour
{
    //[SerializeField] private List<PickupScript> pickups;
    
    [SerializeField] private GameObject Coin;
    [SerializeField] private GameObject Bomb;
    [SerializeField] private GameObject Key;
    [SerializeField] private GameObject Heart;
    [SerializeField] private GameObject Chest;
    [SerializeField] private GameObject GoldenChest;
    void Start()
    {
        float times = Random.Range(0f, 1f);
        if (times > 0.8)
        {
            float choose = Random.Range(0f, 1f);
            if (choose > 0.65)
            {
                Instantiate(GoldenChest, transform.position, Quaternion.identity );
            }
            else
            {
                Instantiate(Chest, transform.position, Quaternion.identity );
            }
        } else if (times > 0.5)
        {
            Instantiate(Key, transform.position, Quaternion.identity );
        }else if (times > 0.3)
        {
            Instantiate(Bomb, transform.position, Quaternion.identity );
        } else if (times > 0.2)
        {
            Instantiate(Coin, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
