using UnityEngine;

namespace ProceduralGeneration
{
    public class ExitPoint : MonoBehaviour
    {
        public bool isConnected = false;
        public Vector3 Forward => transform.forward;
    }
}