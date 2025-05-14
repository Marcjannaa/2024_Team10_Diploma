using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MiniGame.DodgeMiniGame2
{
    public class PlayerCollisions : MonoBehaviour
    {
        [SerializeField] private DodgeMiniGameManager dodgeGameManager;

        private void Start()
        {
            dodgeGameManager = gameObject.GetComponentInParent<DodgeMiniGameManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("MiniGameEnemy")) return;
            dodgeGameManager.SetResult(false);
        }
    }
}
