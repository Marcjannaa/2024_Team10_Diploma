using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRoom : MonoBehaviour
{
    private int _buttonCount;
    private int _pressedButtonCount;
    [SerializeField] private GameObject spikeSet;

    private void Start()
    {
        //doors close
        _pressedButtonCount = 0;
        _buttonCount = transform.Find("Buttons").childCount;
    }

    public void AddPressedButton()
    {
        _pressedButtonCount++;
    }
    
    private void Update()
    {
        if (_pressedButtonCount == _buttonCount)
        {
            spikeSet.SetActive(false);
            //doors open
        }
    }
}
