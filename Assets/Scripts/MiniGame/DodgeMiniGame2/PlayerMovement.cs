using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.DodgeMiniGame2
{
    public class PlayerMovement : MonoBehaviour
    { 
        [SerializeField] private float movementSpeed = 5f;
        private void FixedUpdate()
        {
            var moveX = Input.GetAxisRaw("Horizontal");
            var moveY = Input.GetAxisRaw("Vertical");

            var movement = new Vector3(moveX, moveY, 0f);
            transform.Translate(movement * (movementSpeed * Time.unscaledDeltaTime));
        }
    }
}

