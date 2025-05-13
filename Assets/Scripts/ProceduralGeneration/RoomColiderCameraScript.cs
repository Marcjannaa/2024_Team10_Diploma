using UnityEngine;

namespace ProceduralGeneration
{
    public class RoomColiderCameraScript : MonoBehaviour
    {
        [SerializeField]
        public Camera roomCamera;
        
        
        private void OnTriggerEnter(Collider other)
        {
           
            if (other.CompareTag("Player"))
            {
                var player = other.GetComponent<PlayerController>();
                var room = GetComponentInParent<PlacedRoom>();
                if (player != null && room != null && room.roomForwardTransform != null)
                {
                    player.AlignToRoomDirection(room.roomForwardTransform.forward);
                }
                
                if (roomCamera != null)
                {
                    CameraManager.Instance.SwitchToCamera(roomCamera);
                    Debug.Log("camera switched");
                }
                else
                {
                    Debug.LogWarning("Room camera not assigned for: " + gameObject.name);
                }
            }
        }
    }
}