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

        public ExitPoint GetRandomUnconnectedExit()
        {
            var available = Exits.FindAll(e => !e.isConnected);
            if (available.Count == 0) return null;
            return available[Random.Range(0, available.Count)];
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
    }
}