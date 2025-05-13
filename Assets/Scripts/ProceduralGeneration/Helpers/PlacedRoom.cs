using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProceduralGeneration
{
    public class PlacedRoom : MonoBehaviour
    {
        public RoomConfig definition;
        
        public Transform roomForwardTransform;

        public List<ExitPoint> Exits { get; private set; }

        public void Initialize(RoomConfig def)
        {
            definition = def;
            Exits = new List<ExitPoint>(GetComponentsInChildren<ExitPoint>());
        }


        public BoxCollider GetRoomCollider()
        {
            return GetComponentInChildren<BoxCollider>();
        }

        public List<ExitPoint> GetExits()
        {
            List<ExitPoint> resList = new List<ExitPoint>();
            foreach (var e in GetComponentsInChildren<ExitPoint>())
            {
                resList.Add(e);
            }

            return resList;
        }
        
        public ExitPoint GetClosestExitTo(Vector3 worldPosition)
        {
            return GetExits()
                .OrderBy(e => Vector3.Distance(e.transform.position, worldPosition))
                .FirstOrDefault();
        }

        
        
    }
}