using System.Collections;
using System.Collections.Generic;

namespace ProceduralGeneration
{
    public interface IFloorGenerationStrategy
    {
        
        

        IEnumerator GenerateRooms(List<RoomConfig> roomPool, int countToGenerate, int attempts,
            int maxAttempts, float roomPlacementDelay, List<PlacedRoom> activeRooms, List<ExitPoint> exitPoints,
            FloorGenerator generator);
    }
}