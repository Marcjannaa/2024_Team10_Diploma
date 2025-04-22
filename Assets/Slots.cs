using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    [SerializeField] private GameObject _rewardItem;
    public void Play()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        Instantiate(_rewardItem,pos,Quaternion.identity);
    }
}
