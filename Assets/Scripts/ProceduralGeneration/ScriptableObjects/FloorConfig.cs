using UnityEngine;
using System.Collections.Generic;
using ProceduralGeneration;

[CreateAssetMenu(menuName = "Procedural Generation/Floor Config")]
public class FloorConfig : ScriptableObject
{
    public RoomConfig startingRoom;
    public List<RoomGenerationEntry> roomGenerationData;

    public Dictionary<RoomType, RoomGenerationData> GetGenerationMap()
    {
        var dict = new Dictionary<RoomType, RoomGenerationData>();
        foreach (var entry in roomGenerationData)
        {
            dict[entry.type] = new RoomGenerationData
            {
                rooms = entry.rooms,
                roomsToGenerate = entry.roomsToGenerate
            };
        }
        return dict;
    }
   
}