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