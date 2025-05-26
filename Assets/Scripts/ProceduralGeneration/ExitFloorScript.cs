using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitFloorScript : MonoBehaviour
{
    private bool isOpen = false;
    private Animator anim;
    private void Awake()
    {
         anim = GetComponent<Animator>();
    }

    public void OpenFloorExit()
    {
        isOpen = true;
        anim.SetTrigger("openExitFloorTrigger");
        Debug.Log("Exit is now open!");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isOpen) return;
        
        if (other.CompareTag("Player"))
        {
                LevelManager.Instance.GenerateNextFloor();
        }
    }
}
