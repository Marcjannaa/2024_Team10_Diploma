using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundButton : MonoBehaviour
{
    [SerializeField] GameObject room;
    private TrapRoom trapRoom;
    private bool pressed = false;

    public void Start()
    {
        trapRoom = room.GetComponent<TrapRoom>();
    }
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && !pressed)
        {
            print("OnTriggerEnter");
            transform.Find("Unpressed").gameObject.SetActive(false);
            transform.Find("Pressed").gameObject.SetActive(true);
            pressed = true;
            trapRoom.AddPressedButton();
        }
    }
}
