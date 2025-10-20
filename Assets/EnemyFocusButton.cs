using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFocusButton : MonoBehaviour
{
    private GameObject MyEnemy;

    public void setMyEnemy(GameObject enemy)
    {
        MyEnemy = enemy;
    }

    public GameObject getMyEnemy()
    {
        return MyEnemy;
    }
}
