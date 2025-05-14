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
            float moveX = 0f;
            float moveY = 0f;

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                moveX = -1f;
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                moveX = 1f;

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                moveY = 1f;
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                moveY = -1f;

            var movement = new Vector3(moveX, moveY, 0f);
            transform.Translate(movement * (movementSpeed * Time.unscaledDeltaTime));
        }

    }
}

