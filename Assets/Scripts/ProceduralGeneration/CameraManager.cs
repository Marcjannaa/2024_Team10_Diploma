using System;
using System.Collections.Generic;

namespace ProceduralGeneration
{
    using UnityEngine;

    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }

        private Camera referenceCamera;
        
        private Vector3 _referenceForward;

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
        
        public void AlignCameraToReference(Camera newCam)
        {
            if (referenceCamera == null || newCam == null)
            {
                Debug.LogWarning("Reference camera or new camera is null.");
                return;
            }

            Transform refRoom = referenceCamera.transform.parent;
            Transform newRoom = newCam.transform.parent;

            if (refRoom == null || newRoom == null)
            {
                Debug.LogWarning("Reference or target camera has no parent room.");
                return;
            }

            
            float deltaY = refRoom.eulerAngles.y - newRoom.eulerAngles.y;

            try
            {
                newRoom.transform.root.gameObject.GetComponentInChildren<RoomCenter>().RotateCameraAroundCenter(deltaY, newCam);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
                
            }
            

        }


        
        public void AlignAllCamerasToReference()
        {
            if (referenceCamera == null)
            {
                Debug.LogWarning("Reference camera is not set. Cannot align other cameras.");
                return;
            }

            foreach (Camera cam in allCameras)
            {
                if (cam == referenceCamera) continue;
                
                AlignCameraToReference(cam);
            }

            Debug.Log("All cameras aligned to reference.");
        }
        
        public void SetReferenceCamera(Camera cam)
        {
            referenceCamera = cam;
        }
    }

}