using UnityEngine;
using UnityEngine.Events;

namespace ProceduralGeneration
{
    public class EnemyOnDefeatComponent  : MonoBehaviour
    {
        public UnityEvent OnEnemyDefeated;
        private void OnDestroy()
        {
            OnEnemyDefeated?.Invoke();
        }
    }
}