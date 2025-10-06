using UnityEngine;

namespace ProceduralGeneration
{
    public class DoorHandlingComponent : MonoBehaviour
    {
        private Animator anim;
        
        private void Awake()
        {
            anim = GetComponent<Animator>();
        }
        
        public void OpenRoomExit()
        {
           
            anim.SetTrigger("ArcDoorOpenTrigger");
            Debug.Log("room in now open");
        }
        
        public void CloseRoomExit()
        {
            anim.SetTrigger("ArcDoorCloseTrigger");
            Debug.Log("room in now closed");
        }
    }
}