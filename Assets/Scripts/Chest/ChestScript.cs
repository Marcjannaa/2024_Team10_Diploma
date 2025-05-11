using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChestScript : MonoBehaviour
{
    [SerializeField] private bool requireKey;
    [SerializeField] private GameObject Coin;
    [SerializeField] private GameObject Bomb;
    [SerializeField] private GameObject Key;
    [SerializeField] private List<Item> items;
    private Vector3 position;
    void Start()
    {
        position = transform.position;
    }

    void Open()
    {
        
        
        if (requireKey)
        {
            
                if (Player_Stats.Keys.Value > 0 || Player_Stats.LockPick.getFlag())
                {
                    if(!Player_Stats.LockPick.getFlag())
                        Player_Stats.Keys.Modify(-1);
                    float times = Random.Range(0f, 1f);
                    int choose;
                    int loopCount;
                    Debug.Log(times);
                    if (times > 0.9)
                    {
                        loopCount = 0;
                        int choice = Random.Range( 0, items.Count);
                        Instantiate(items[choice]);
                    } else if (times > 0.8)
                    {
                        loopCount = 4;
                    } else if (times > 0.5)
                    {
                        loopCount = 3;
                    } else
                        loopCount = 2;
                
                    for (int i = 0; i < loopCount; i++)
                    {
                        times = Random.Range(0f, 1f);
                        choose = Random.Range(1, 4);
                        switch (choose)
                        {
                            case 1:
                                Instantiate(Coin, new Vector3(position.x + times, Coin.transform.position.y, position.z - times), new Quaternion());
                                break;
                            case 2:
                                Instantiate(Bomb, new Vector3(position.x + times, Bomb.transform.position.y, position.z + times), new Quaternion());
                                break;
                            case 3:
                                Instantiate(Key, new Vector3(position.x - times, Key.transform.position.y, position.z + times), new Quaternion());
                                break;
                        }
                    }
                    Destroy(gameObject);
                }
        }
        else
        {
            float times = Random.Range(0f, 1f);
            int choose;
            int loopCount;
            Debug.Log(times);

            if (times > 0.8)
            {
                loopCount = 3;
            } else if (times > 0.5)
            {
                loopCount = 2;
            } else
                loopCount = 1;
            
            for (int i = 0; i < loopCount; i++)
            {
                times = Random.Range(0f, 1f);
                choose = Random.Range(1, 4);
                switch (choose)
                {
                    case 1:
                        Instantiate(Coin, new Vector3(position.x + times, Coin.transform.position.y, position.z - times), new Quaternion());
                        break;
                    case 2:
                        Instantiate(Bomb, new Vector3(position.x + times, Bomb.transform.position.y, position.z + times), new Quaternion());
                        break;
                    case 3:
                        Instantiate(Key, new Vector3(position.x - times, Key.transform.position.y, position.z + times), new Quaternion());
                        break;
                }
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.tag);
        if (other.collider.tag.Equals("Player"))
        {
            Open();
        }
    }
}
