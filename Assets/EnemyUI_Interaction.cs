using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI_Interaction : MonoBehaviour
{
    private GameObject _enemyUIComponent;

    public void setUIComponent(GameObject cmp)
    {
        _enemyUIComponent = cmp;
    }

    public GameObject getUIComponent()
    {
        return _enemyUIComponent;
    }
}
