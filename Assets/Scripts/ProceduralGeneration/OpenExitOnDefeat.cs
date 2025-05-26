using System;
using UnityEngine;
using UnityEngine.Events;

namespace ProceduralGeneration
{
    
    public class OpenExitOnDefeat : MonoBehaviour
    {
        public UnityEvent OnBossDefeated;
        private void OnDestroy()
        {
            OnBossDefeated?.Invoke();
        }
    }
}