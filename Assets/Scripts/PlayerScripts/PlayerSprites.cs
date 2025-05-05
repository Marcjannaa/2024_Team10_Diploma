using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprites : MonoBehaviour
{
    public void LookLeft(bool isLeft)
    {
        var theScale = transform.localScale;
        theScale.x = isLeft ? -1f : 1f;
        transform.localScale = theScale;
    }
}
