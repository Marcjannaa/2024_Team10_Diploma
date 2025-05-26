using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProceduralGeneration
{
    public static class RoomPlacementHelper
    {
        public static PlacedRoom PlaceRoom(RoomConfig config, Vector3 position, Quaternion rotation)
        {
            if (config?.prefab is null)
            {
                Debug.LogError("RoomConfig or prefab is null.");
                return null;
            }

            GameObject roomObj = Object.Instantiate(config.prefab, position, rotation);
            PlacedRoom placed = roomObj.GetComponentInChildren<PlacedRoom>();
            if (placed is null)
            {
                Debug.LogError("PlacedRoom component missing.");
                Object.Destroy(roomObj);
                return null;
            }

            Debug.Log($"PlacedRoom found.");
            placed.Initialize(config);
            return placed;
        }

        public static bool IsRoomOverlapping(PlacedRoom room, List<BoxCollider> existingColliders)
        {
            BoxCollider colA = room.GetRoomCollider();
            if (colA is null)
            {
                Debug.LogError($"Room {room.name} missing BoxCollider!");
                return false;
            }

            return existingColliders.Any(colB => colB is not null && colA.bounds.Intersects(colB.bounds));
        }

        public static ExitPoint GetRandomUnconnectedExit(List<ExitPoint> exitPoints)
        {
            var unconnected = exitPoints.Where(e => !e.isConnected).ToList();
            if (unconnected.Count == 0)
            {
                Debug.LogWarning("No unconnected exits available.");
                return null;
            }

            return unconnected[Random.Range(0, unconnected.Count)];
        }

        public static RoomConfig GetRandomRoom(List<RoomConfig> roomList)
        {
            var valid = roomList.Where(r => r is not null).ToList();
            if (valid.Count == 0)
            {
                Debug.LogError("No valid  rooms.");
                return null;
            }

            return valid[Random.Range(0, valid.Count)];
        }
        public static RoomConfig GetRandomRoomWithBias(List<RoomConfig> roomList, RoomDifficulty favoredDifficulty, float biasProbability)
        {
            var valid = roomList.Where(r => r != null).ToList();
            if (valid.Count == 0)
            {
                Debug.LogError("No valid  rooms.");
                return null;
            }
            
            float roll = Random.value;
            if (roll < biasProbability)
            {
                var favored = valid.Where(r => r.difficulty == favoredDifficulty).ToList();
                if (favored.Count > 0)
                    return favored[Random.Range(0, favored.Count)];
            }

            return valid[Random.Range(0, valid.Count)];
        }

        public static ExitPoint FindNearestExit(ExitPoint source, List<ExitPoint> candidates)
        {
            return candidates
                .Where(e => e != source)
                .OrderBy(e => Vector3.Distance(source.transform.position, e.transform.position))
                .FirstOrDefault();
        }

        public static void CloseNearestExit(ExitPoint exit, List<ExitPoint> allExits)
        {
            ExitPoint nearest = FindNearestExit(exit, allExits);
            if (nearest is not null)
                nearest.isConnected = true;
        }
        
        public static void AddRoomExits(PlacedRoom room, List<ExitPoint> exitList)
        {
            exitList.AddRange(room.GetExits());
        }
    }
}