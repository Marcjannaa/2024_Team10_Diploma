using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.DodgeMiniGame2
{
    public class DodgeMiniGameManager : MonoBehaviour
    {
        [SerializeField] private float miniGameTime = 5f;
        [SerializeField] private GameObject cam;
        
        private float _gameTime;
        private bool _result;

        private void OnEnable()
        {
            _gameTime = 0f;
            cam.SetActive(true);
        }

        private void Update()
        {
            _gameTime += Time.unscaledDeltaTime;
            if (_gameTime >= miniGameTime)
                CombatManager.OnDodgeEnded(true);
        }
        
        public float GetMiniGameTime()
        {
            return miniGameTime;
        }

        public void SetResult(bool val)
        {
            CombatManager.OnDodgeEnded(val);
        }
    }
}
