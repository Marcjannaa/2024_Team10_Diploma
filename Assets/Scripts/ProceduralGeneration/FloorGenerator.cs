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
        private FloorConfig floorConfig;
        private List<PlacedRoom> activeRooms = new();
        private List<BoxCollider> colliders = new();
        private List<ExitPoint> exitPoints = new();
        private int attempts = 0;
        [SerializeField] private float roomPlacementDelay = 0;
        public UnityEvent OnFloorGenerated;
        public void GenerateFloor(FloorConfig config)
        {
            floorConfig = config;

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
                    GenerateRooms(entry.Value.rooms, entry.Value.roomsToGenerate, attempts, maxGlobalAttempts)
                );
            }
            
            CloseUnconnectedExits();
            OnFloorGenerated?.Invoke(); 
        }

        private IEnumerator GenerateRooms(List<RoomConfig> roomPool, int countToGenerate, int attempts, int maxAttempts)
        {
            int roomsPlaced = 0;

            while (roomsPlaced < countToGenerate && attempts < maxAttempts)
            {
                attempts++;
                Debug.Log($"Attempts: {attempts}, Active rooms: {activeRooms.Count + 1}");

                var selectedExit = RoomPlacementHelper.GetRandomUnconnectedExit(exitPoints);
                if (selectedExit is null)
                {
                    Debug.LogWarning("No valid exit point found, aborting.");
                    yield break;
                }

                RoomConfig roomDef = RoomPlacementHelper.GetRandomRoom(roomPool);
                if (roomDef is null)
                {
                    Debug.LogWarning("No valid room config, aborting.");
                    yield break;
                }

                int initialCount = activeRooms.Count;
                TryPlaceRoomAtExit(selectedExit, roomDef);
                if (activeRooms.Count > initialCount)
                {
                    roomsPlaced++;
                }

                yield return new WaitForSeconds(roomPlacementDelay);
            }
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


        private void TryPlaceRoomAtExit(ExitPoint exit, RoomConfig roomDef)
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
    }
}