using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralGeneration
{
    public class StandardGenerationStrategy : IFloorGenerationStrategy
    {
        
        IEnumerator IFloorGenerationStrategy.GenerateRooms(List<RoomConfig> roomPool, int countToGenerate, int attempts,
            int maxAttempts, float roomPlacementDelay, List<PlacedRoom> activeRooms,List<ExitPoint> exitPoints, FloorGenerator generator )
        {
            
            
            int roomsPlaced = 0;

            while (roomsPlaced < countToGenerate && attempts < maxAttempts)
            {
                attempts++;

                var selectedExit = RoomPlacementHelper.GetRandomUnconnectedExit(exitPoints);
                if (selectedExit is null)
                {
                    yield break;
                }

                RoomConfig roomDef = RoomPlacementHelper.GetRandomRoom(roomPool);
                if (roomDef is null)
                {
                    yield break;
                }

                int initialCount = activeRooms.Count;
                generator.TryPlaceRoomAtExit(selectedExit, roomDef);
                if (activeRooms.Count > initialCount)
                {
                    roomsPlaced++;
                }

                yield return new WaitForSeconds(roomPlacementDelay);
            }
        }
    }
}