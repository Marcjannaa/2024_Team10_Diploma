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
        public bool isOverlapped = false;

        public void activateWall()
        {
            if (exitwall is null)
            {
                throw new MissingComponentException(" arc is null");
            }
            exitwall.gameObject.SetActive(true);
        }

        public void deactivateArc()
        {
            if (arc is  null)
            {
                throw new MissingComponentException(" arc is null");
            }
            arc.gameObject.SetActive(false);
        }
        
    }
}