using System.Collections.Generic;

namespace ProceduralGeneration
{
    [System.Serializable]
    public class RoomGenerationEntry
    {
        public RoomType type;
        public List<RoomConfig> rooms;
        public int roomsToGenerate;
    }
}