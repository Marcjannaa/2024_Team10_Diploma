using System;
using Unity.VisualScripting;
using UnityEngine;

namespace ProceduralGeneration
{
    public class RoomColiderCompletionComponent : MonoBehaviour
    {
        private bool isRoomComplete = false;
        
        [SerializeField] private GameObject[] enemyList;
        [SerializeField] private GameObject[] exitList;
        private int enemyCount;
        private void Awake()
        {
            enemyCount = enemyList.Length;
        }
        
       
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (isRoomComplete)
                {
                    return;
                }

                if (enemyCount <= 0)
                {
                    return;
                }
                foreach (var door in exitList)
                {
                    door.GetComponent<DoorHandlingComponent>().CloseRoomExit();
                
                }
            }
           
        }

        public void EnemyKilled()
        {
            if (enemyCount > 1)
            {
                enemyCount--;
            }
            else
            {
                isRoomComplete = true;

                foreach (var door in exitList)
                {
                    door.GetComponent<DoorHandlingComponent>().OpenRoomExit();
                }
            }
            
        }
    }
}