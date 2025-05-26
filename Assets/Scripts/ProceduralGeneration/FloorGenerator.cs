using System;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;

namespace ProceduralGeneration
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    public class FloorGenerator : MonoBehaviour
    {
        private IFloorGenerationStrategy currentGenerationStrategy;
        private FloorConfig floorConfig;
        private List<PlacedRoom> activeRooms = new();
        private List<BoxCollider> colliders = new();
        private List<ExitPoint> exitPoints = new();
        private int attempts = 0;
        [SerializeField] private float roomPlacementDelay = 0;
        public UnityEvent OnFloorGenerated;
        public void GenerateFloor(FloorConfig config, IFloorGenerationStrategy strategy)
        {
            floorConfig = config;
            currentGenerationStrategy = strategy;

            // first room gen
            ClearPreviousFloor();
            var startRoom = Instantiate(floorConfig.startingRoom.prefab);
            var placedStart = startRoom.GetComponentInChildren<PlacedRoom>();
            placedStart.Initialize(floorConfig.startingRoom);

            activeRooms.Add(placedStart);
            colliders.Add(placedStart.GetRoomCollider());
            RoomPlacementHelper.AddRoomExits(placedStart, exitPoints);

            StartCoroutine(GenerateLoop());
        }

        private IEnumerator GenerateLoop()
        {
            int maxGlobalAttempts = 200;
            
            var generationMap = floorConfig.GetGenerationMap();

            foreach (var entry in generationMap)
            {
                yield return StartCoroutine(
                    //GenerateRooms(entry.Value.rooms, entry.Value.roomsToGenerate, attempts, maxGlobalAttempts)
                    currentGenerationStrategy.GenerateRooms(entry.Value.rooms, entry.Value.roomsToGenerate, attempts, maxGlobalAttempts, 
                        roomPlacementDelay, activeRooms , exitPoints, this)
                );
            }
            
            CloseUnconnectedExits();
            OnFloorGenerated?.Invoke(); 
        }
        
        private void ClearPreviousFloor()
        {
            foreach (var room in activeRooms)
            {
                if (room is not null)
                    Destroy(room.transform.root.gameObject);
            }

            activeRooms.Clear();
            colliders.Clear();
            exitPoints.Clear();
        }

        private void CloseUnconnectedExits()
        {
            foreach (var exit in exitPoints)
            {
                if (!exit.isConnected)
                {
                    exit.activateWall();
                    exit.deactivateArc();
                }
            }
        }


        public void TryPlaceRoomAtExit(ExitPoint exit, RoomConfig roomDef)
        {
            if (exit is null || roomDef is null)
            {
                Debug.LogWarning("Exit or RoomConfig is null, skipping room placement.");
                return;
            }
            
            var room = RoomPlacementHelper.PlaceRoom(roomDef, exit.transform.position, exit.transform.rotation);
            
            if (room is null)
            {
                Debug.LogWarning("Failed to place room.");
                return;
            }
           
            
            if (RoomPlacementHelper.IsRoomOverlapping(room, colliders))
            {
                Destroy(room.transform.root.gameObject);

                exit.isConnected = true;
                Debug.Log("Overlap detected, room destroyed.");
            }
            else
            {
                activeRooms.Add(room);
                colliders.Add(room.GetRoomCollider());
                RoomPlacementHelper.AddRoomExits(room, exitPoints);
                exit.isConnected = true;
                RoomPlacementHelper.CloseNearestExit(exit, exitPoints);
            }
        }

        public void SetFloorGenerationStrategy(IFloorGenerationStrategy strategy)
        {
            currentGenerationStrategy = strategy;
        }

        public List<PlacedRoom> GetActiveRoomList()
        {
            return activeRooms;
        }
        
    }
}