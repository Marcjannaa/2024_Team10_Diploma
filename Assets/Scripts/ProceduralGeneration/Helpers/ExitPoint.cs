using UnityEngine;

namespace ProceduralGeneration
{
    public class ExitPoint : MonoBehaviour
    {
        [SerializeField]
        public GameObject exitwall;
        [SerializeField]
        public GameObject arc;
        public bool isConnected = false;

        public void activateWall()
        {
            if (exitwall is not null)
            {
                exitwall.gameObject.SetActive(true);
            }
        }

        public void deactivateArc()
        {
            if (arc is not null)
            {
                arc.gameObject.SetActive(false);
            }
        }
        
    }
}