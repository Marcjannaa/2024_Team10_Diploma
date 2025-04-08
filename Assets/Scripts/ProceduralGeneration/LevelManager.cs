using UnityEngine;

namespace ProceduralGeneration
{
     public class LevelManager : MonoBehaviour
    {
        [SerializeField] private FloorGenerator floorGenerator;

        private void Start()
        {
            floorGenerator.Initialize(this);
            StartNewFloor();
        }

        public void StartNewFloor()
        {
            floorGenerator.GenerateFloor();
        }

        public void OnFloorGenerationComplete()
        {
            Debug.Log("Floor generation complete!");
        }
    }
}