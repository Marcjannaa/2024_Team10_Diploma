using UnityEngine;

[CreateAssetMenu(fileName = "MinimapIconSet", menuName = "Minimap/Icon Set")]
public class MinimapIconSet : ScriptableObject
{
    [Header("Minimap Sprites")]
    public Sprite defaultRoom;
    public Sprite startRoom;
    public Sprite bossRoom;
    public Sprite treasureRoom;
    public Sprite shopRoom;
    public Sprite casinoRoom;
    public Sprite trapRoom;

    public Sprite GetSprite(RoomTypeMinimap type)
    {
        return type switch
        {
            RoomTypeMinimap.Start => startRoom,
            RoomTypeMinimap.Boss => bossRoom,
            RoomTypeMinimap.Treasure => treasureRoom,
            RoomTypeMinimap.Shop => shopRoom,
            RoomTypeMinimap.Casino => casinoRoom,
            RoomTypeMinimap.Trap => trapRoom,
            _ => defaultRoom
        };
    }
}

public enum RoomTypeMinimap
{
    Default,
    Start,
    Boss,
    Treasure,
    Shop,
    Casino,
    Trap
}