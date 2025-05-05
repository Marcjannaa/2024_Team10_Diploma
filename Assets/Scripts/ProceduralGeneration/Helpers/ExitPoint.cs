using UnityEngine;

namespace ProceduralGeneration
{
    public class ExitPoint : MonoBehaviour
    {
        [SerializeField]
        public GameObject exitwall;
        public bool isConnected = false;

        public void activateWall()
        {
            if (exitwall is not null)
            {
                exitwall.gameObject.SetActive(true);
            }
        }
        
    }
}