using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Procedural Generation/Floor Config")]
public class FloorConfig : ScriptableObject
{
    public RoomConfig startingRoom;
    public List<RoomConfig> standardRooms;
    public List<RoomConfig> treasureRooms;
    public List<RoomConfig> bossRooms;

    public int standardRoomsToGenerate = 10;
    public int treasureRoomsToGenerate = 1;
    public int bossRoomsToGenerate = 1;
}