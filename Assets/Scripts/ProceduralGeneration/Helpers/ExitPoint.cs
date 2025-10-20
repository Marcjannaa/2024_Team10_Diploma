using System;
using UnityEngine;

namespace ProceduralGeneration
{
    public class ExitPoint : MonoBehaviour
    {
        [SerializeField]
        public GameObject exitwall;
        [SerializeField]
        public GameObject arc;
        public bool isConnected = false;
        public ExitPoint ConnectedExit { get; set; }
        public bool isOverlapped = false;
        private PlacedRoom _placedRoom;

        public void Awake()
        {
            _placedRoom = GetComponentInParent<PlacedRoom>();
            if (_placedRoom == null)
                Debug.LogWarning($"[ExitPoint] {name} has no PlacedRoom parent!");
        }

        public void activateWall()
        {
            if (exitwall is null)
            {
                throw new MissingComponentException(" arc is null");
            }
            exitwall.gameObject.SetActive(true);
        }

        public void deactivateArc()
        {
            if (arc is  null)
            {
                throw new MissingComponentException(" arc is null");
            }
            arc.gameObject.SetActive(false);
        }
        
        public PlacedRoom GetPlacedRoom()
        {
            return _placedRoom;
        }
    }
}