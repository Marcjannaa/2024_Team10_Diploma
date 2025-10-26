using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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

        public List<PlacedRoom> GetConnectedRooms()
        {
            var connectedRooms = new List<PlacedRoom>();
            foreach (var exit in Exits)
            {
                if (exit.isConnected && exit != null)
                {

                    if (exit.ConnectedExit is null)
                    {
                        Debug.LogError("Connected exit is null despite isConnected being true. " +
                                       exit.gameObject.name + " in room " + PrefabUtility.GetNearestPrefabInstanceRoot(this).gameObject.name);
                        continue;
                    }
                    if ( exit.ConnectedExit.GetPlacedRoom() is null)
                    {
                        Debug.LogError("Connected exit's placed room is null.");

                        continue;
                    }
                    connectedRooms.Add(exit.ConnectedExit.GetPlacedRoom());
                }
            }
            return connectedRooms;
        }

        public void setActive() {
            SetRoomActive(true);
        }
        
        public void setInactive() {
            SetRoomActive(false); 
        }
        
        private void SetRoomActive(bool isActive)
        {
          
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = isActive;
            }
            
        }
        //TODO: Add real references beetwen connected exits - rooms even. what was i thinking !>?? ??/
    }
}