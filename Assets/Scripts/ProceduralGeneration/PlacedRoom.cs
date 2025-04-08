using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProceduralGeneration
{
    public class PlacedRoom : MonoBehaviour
    {
        public RoomConfig definition;
        
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
        
        public void DestroyWithAllChildren()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            
            Destroy(gameObject);
        }
        
    }
}