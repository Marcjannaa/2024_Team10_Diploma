using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundButton : MonoBehaviour
{
    private GameObject _spikeSet;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("OnTriggerEnter");
            transform.Find("Unpressed").gameObject.SetActive(false);
            transform.Find("Pressed").gameObject.SetActive(true);
            
            _spikeSet.SetActive(false);
        }
    }
}
