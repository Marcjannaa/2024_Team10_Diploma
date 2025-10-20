using System.Collections;
using System.Collections.Generic;
using ProceduralGeneration;
using UnityEngine;

public class MinimapFollowScript : MonoBehaviour
{
    public float height = 50f;

    public void MinimapUpdatePosition(PlacedRoom pr)
    {
        transform.position = new Vector3(pr.transform.position.x, height, pr.transform.position.z);
    }
}