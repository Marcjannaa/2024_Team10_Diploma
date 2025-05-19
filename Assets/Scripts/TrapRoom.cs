using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRoom : MonoBehaviour
{
    private int _buttonCount;
    private int _pressedButtonCount;
    [SerializeField] private GameObject rewardPosition;
    [SerializeField] private GameObject spikeSet;
    [SerializeField] private GameObject turretSet;
    private List<Turret> _turrets = new List<Turret>();
    [SerializeField] private GameObject rewardItem;
    private bool _rewardSpawned;

    private void Start()
    {
        //doors close
        _pressedButtonCount = 0;
        _buttonCount = transform.Find("Buttons").childCount;
        _turrets.AddRange(turretSet.GetComponentsInChildren<Turret>());
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
            foreach (var turret in _turrets)
            {
                turret.DeactivateShooting();
            }
            if (!_rewardSpawned)
            {
                Instantiate(rewardItem, rewardPosition.transform.position, Quaternion.identity);
                _rewardSpawned = true;
            }
        }
    }
}
