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
        private List<ExitPoint> ExitPoints = new();
        [SerializeField] int roomsToGenerate = 10;

        public void Initialize(LevelManager manager)
        {
            levelManager = manager;
        }

        public void GenerateFloor()
        {
            ClearPrevious();

            var startRoom = Instantiate(startingRoom.prefab);
            var placedStart = startRoom.GetComponentInChildren<PlacedRoom>();
            placedStart.Initialize(startingRoom);
            
            activeRooms.Add(placedStart);
            Colliders.Add(placedStart.GetRoomCollider());
            AddRoomExitsToList(placedStart);
            
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

                var selectedExit = GetRandomExit();
                RoomConfig newRoomDef = GetRandomStandardRoomDef();
                GameObject prefab = newRoomDef.prefab;

                TryPlaceRoomAtExit(prefab, selectedExit, newRoomDef);
                
                yield return new WaitForSeconds(1f);
            }

            levelManager.OnFloorGenerationComplete();
        }


        private void ClearPrevious()
        {
            foreach (var room in activeRooms)
            {
                if (room is not null)
                    Destroy(room.gameObject);
            }

            activeRooms.Clear();
            Colliders.Clear();
        }

        private ExitPoint GetRandomExit()
        {
            if (ExitPoints.Count == 0)
            {
                Debug.LogError("No exits available to generate new rooms.");
                return null;
            }

            ExitPoint exit = null;
            bool connected = true;
            while (connected)
            {
                 exit = ExitPoints[Random.Range(0, ExitPoints.Count)];
                 connected = exit.isConnected; //#todo potencjalnie infinite loop do zmiany potem.
            }
            Debug.Log($"Selected exit: {exit}");
            return exit;
        }


        private RoomConfig GetRandomStandardRoomDef()
        {
            if (standardRooms.Count == 0)
            {
                Debug.LogError("No standard rooms available.");
                return null;
            }

            // Filter out null rooms first
            var validRooms = standardRooms.Where(r => r != null).ToList();
            if (validRooms.Count == 0)
            {
                Debug.LogError("No valid rooms available.");
                return null;
            }

            Debug.Log("Selected room def.");
            return validRooms[Random.Range(0, validRooms.Count)];
        }


        private void TryPlaceRoomAtExit(GameObject prefab, ExitPoint targetExit, RoomConfig def)
        {
            if(targetExit is null)  Debug.LogError("Target exit is null.");
            
            GameObject newRoom = Instantiate(prefab);
            var placedRoom = newRoom.GetComponentInChildren<PlacedRoom>();
            placedRoom.transform.position = targetExit.transform.position;
            placedRoom.transform.rotation = targetExit.transform.rotation;
            Debug.Log($"Placing room at {placedRoom.transform.position} with rotation {placedRoom.transform.rotation}");
            
            placedRoom.Initialize(def);
            
            if (IsRoomOverlapping(placedRoom))
            {
                Destroy(newRoom);
               Debug.Log("Room Overlaping detected destroying");
                
            }
            else
            {
                activeRooms.Add(placedRoom);
                Colliders.Add(placedRoom.GetRoomCollider());
                AddRoomExitsToList(placedRoom);
                targetExit.isConnected = true; 
                CloseNearestExit(targetExit);
                Debug.Log("Room placed");
        
            }
            
        }


        private bool IsRoomOverlapping(PlacedRoom room)
        {
            var ColA = room.GetRoomCollider();

            if (ColA == null)
            {
                Debug.LogError($"Room {room.gameObject.name} has no BoxCollider for overlap detection!");
                return false; // Or handle this differently based on your logic
            }

            foreach (var colB in Colliders)
            {
                if (colB != null && ColA.bounds.Intersects(colB.bounds))
                {
                    return true;
                }
            }
            print("no overlap detected");
            return false;
        }

        private void AddRoomExitsToList(PlacedRoom room)
        {
            foreach (var exit in room.GetExits())
            {
                ExitPoints.Add(exit);
                
            }
            
        }
       
        public ExitPoint FindNearestExit (ExitPoint exit, List<ExitPoint> Exits)
        {
            ExitPoint nearestObject = null;
            float nearestDistance = Mathf.Infinity;

            // Loop through the list of objects
            foreach (ExitPoint e in Exits)
            {
                float distance = Vector3.Distance(exit.transform.position, e.transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestObject = e;
                }
            }
           
            return nearestObject;
        }

        public void CloseNearestExit(ExitPoint exit)
        {
            ExitPoint exitToClose = FindNearestExit(exit, ExitPoints);
            exitToClose.isConnected = true;
        }
    }
    
}