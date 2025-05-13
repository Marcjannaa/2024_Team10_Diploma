using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.DodgeMiniGame2
{
    public class PlayerCollisions : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("MiniGameEnemy")) return;
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
