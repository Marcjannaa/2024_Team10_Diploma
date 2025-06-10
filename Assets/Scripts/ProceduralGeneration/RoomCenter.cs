using System.Collections.Generic;
using UnityEngine;

namespace ProceduralGeneration
{
    public class RoomCenter : MonoBehaviour
    {
        [SerializeField] public List<Enemy> enemies;
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

            for (int i = 0; i <= enemies.Count; i++)
            {
                enemies[i].LookAtGameobject(obj.gameObject);
            }
        }
    }
}