using System.Collections.Generic;

namespace ProceduralGeneration
{
    using UnityEngine;

    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }

        private List<Camera> allCameras = new List<Camera>();
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        public void InitializeCameraList()
        {
            allCameras.Clear();
            allCameras.AddRange(FindObjectsOfType<Camera>(true)); 
            Debug.Log("Cameras Intitialized");
        }

        public void SwitchToCamera(Camera targetCamera)
        {
            if (targetCamera == null)
            {
                Debug.LogWarning("SwitchToCamera called with null targetCamera.");
                return;
            }

            foreach (Camera cam in allCameras)
            {
                cam.gameObject.SetActive(false);
            }

            targetCamera.gameObject.SetActive(true);
        }
    }

}