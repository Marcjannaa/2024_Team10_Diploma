using UnityEngine;

namespace ProceduralGeneration
{
    public class RoomCenter : MonoBehaviour
    {
        public void RotateCameraAroundCenter(float angle, Camera obj)
        {
            if (obj == null)
            {
                Debug.LogWarning("RotateAroundCenter: Camera is null.");
                return;
            }

            Transform camTransform = obj.transform;
            Vector3 pivot = this.transform.position; // Center of the room
            Vector3 axis = Vector3.up; // Rotate around the Y axis

            camTransform.RotateAround(pivot, axis, angle);
        }
    }
}