using System;
using UnityEngine;

namespace ProceduralGeneration
{
    public class RoomColiderRoomActivationComponent : MonoBehaviour
    {
        private LevelManager _levelManager;
        
        private void Awake()
        {
            _levelManager = LevelManager.Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
           
            if (other.CompareTag("Player"))
            {
                var placedRoom = GetComponentInParent<PlacedRoom>();
                if (placedRoom != null)
                {
                     _levelManager.setActiveRoom(placedRoom);
                     Debug.Log("Room activated trigger triggered succesfuly: " );
                }
                else
                {
                     Debug.LogWarning("PlacedRoom component not found in parent hierarchy.");
                }
            }
        }
    }
}