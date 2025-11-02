using System.Collections;
using System.Collections.Generic;
using ProceduralGeneration;
using Unity.VisualScripting;
using UnityEngine;

public class MinimapFollowScript : MonoBehaviour
{
    public float height = 50f;

    public void MinimapUpdatePosition(PlacedRoom pr)
    {
        var newpos = pr.transform.parent.GetComponentInChildren<RoomCenter>().transform.position;
        transform.position = new Vector3(newpos.x, height, newpos.z);
    }
}