using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon/Room Config")]
public class RoomConfig : ScriptableObject
{
    public string roomName;
    public GameObject prefab;
    public bool isSpecialRoom;
    public bool isBossRoom;
}
