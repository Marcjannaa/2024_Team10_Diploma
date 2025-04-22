using System.Collections;
using System.Collections.Generic;
using ProceduralGeneration;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon/Room Config")]
public class RoomConfig : ScriptableObject
{
    public GameObject prefab;
    public RoomType roomType;
}
