using UnityEngine;
using UnityEngine.Events;

namespace ProceduralGeneration
{
    public class EnemyOnDefeatScript  : MonoBehaviour
    {
        public UnityEvent OnEnemyDefeated;
        private void OnDestroy()
        {
            OnEnemyDefeated?.Invoke();
        }
    }
}