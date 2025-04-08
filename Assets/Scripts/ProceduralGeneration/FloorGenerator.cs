using System.Linq;
using UnityEngine.SocialPlatforms;

namespace ProceduralGeneration
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    public class FloorGenerator : MonoBehaviour
    {
        public RoomConfig startingRoom;
        public List<RoomConfig> standardRooms;
        private LevelManager levelManager;
        private List<PlacedRoom> activeRooms = new();
        private List<BoxCollider> Colliders = new();
        private List<ExitPoint> exitPoints = new();
        [SerializeField] int roomsToGenerate = 10;

        public void Initialize(LevelManager manager)
        {
            levelManager = manager;
        }

        public void GenerateFloor()
        {
            ClearPreviousFloor();

            var startRoom = Instantiate(startingRoom.prefab);
            var placedStart = startRoom.GetComponentInChildren<PlacedRoom>();
            placedStart.Initialize(startingRoom);

            activeRooms.Add(placedStart);
            Colliders.Add(placedStart.GetRoomCollider());
            RoomPlacementHelper.AddRoomExits(placedStart,exitPoints);

            StartCoroutine(GenerateLoop());
        }

        private IEnumerator GenerateLoop()
        {
            int attempts = 0;
            int maxGlobalAttempts = 200;
            print($"Attempts: {attempts}, Active rooms: {activeRooms.Count}");

            while (activeRooms.Count < roomsToGenerate && attempts < maxGlobalAttempts)
            {
                attempts++;

                var selectedExit = RoomPlacementHelper.GetRandomUnconnectedExit(exitPoints);
                RoomConfig newRoomDef = RoomPlacementHelper.GetRandomRoom(standardRooms); // #todo add generating special rooms etc
                TryPlaceRoomAtExit(selectedExit, newRoomDef);

                yield return new WaitForSeconds(0.5f);
            }

            levelManager.OnFloorGenerationComplete();
        }


        private void ClearPreviousFloor()
        {
            foreach (var room in activeRooms)
            {
                if (room is not null)
                    room.DestroyWithAllChildren();
            }

            activeRooms.Clear();
            Colliders.Clear();
            exitPoints.Clear();
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

            if (RoomPlacementHelper.IsRoomOverlapping(room, Colliders))
            {
                Object.Destroy(room);
                exit.isConnected = true;
                Debug.Log("Overlap detected, room destroyed.");
            }
            else
            {
                activeRooms.Add(room);
                Colliders.Add(room.GetRoomCollider());
                RoomPlacementHelper.AddRoomExits(room, exitPoints);
                exit.isConnected = true;
                RoomPlacementHelper.CloseNearestExit(exit, exitPoints);
            }
        }

        
    }
}