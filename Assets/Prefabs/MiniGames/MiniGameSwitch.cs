using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameSwitch : MonoBehaviour
{
    [SerializeField] private GameObject dodgeMiniGame;

    private void Awake()
    {
        dodgeMiniGame.SetActive(true);
    }

    private void OnDisable()
    {
        dodgeMiniGame.SetActive(false);
    }

}
