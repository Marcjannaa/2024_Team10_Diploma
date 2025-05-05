using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundButton : MonoBehaviour
{
    [SerializeField] GameObject room;
    private TrapRoom trapRoom;

    public void Start()
    {
        trapRoom = room.GetComponent<TrapRoom>();
    }
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            print("OnTriggerEnter");
            transform.Find("Unpressed").gameObject.SetActive(false);
            transform.Find("Pressed").gameObject.SetActive(true);
            
            trapRoom.AddPressedButton();
        }
    }
}
