using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesCollisions : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        print("wykrywa");
        if (other.gameObject.CompareTag("Player"))
        {
            print("ała");
            Player_Stats.Health.Modify(-30);
        }
    }
}
