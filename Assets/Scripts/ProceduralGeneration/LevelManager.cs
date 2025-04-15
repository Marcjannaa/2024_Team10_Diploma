using UnityEngine;
using System.Collections.Generic;
using ProceduralGeneration;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [SerializeField] private FloorGenerator _floorGenerator;
    [SerializeField] private List<FloorConfig> floorConfigs;
    
    private int currentFloorIndex = 0;
    private bool HasMoreFloors => currentFloorIndex < floorConfigs.Count;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (_floorGenerator is null)
        {
            Debug.LogError("FloorGenerator reference not assigned!");
            return;
        }
        
        _floorGenerator.OnFloorGenerated.AddListener(OnFloorGenerationComplete);
        GenerateNextFloor(); 
        // #TODO move the start of creating new floor to game manager if made and if not just call it 
        // in the boss room exit script when made
    }

    public void GenerateNextFloor()
    {
        if (!HasMoreFloors)
        {
            Debug.Log("All floors generated.");
            return;
        }

        FloorConfig config = floorConfigs[currentFloorIndex];
        _floorGenerator.GenerateFloor(config);
    }

    public void OnFloorGenerationComplete()
    {
        Debug.Log($"Floor {currentFloorIndex + 1} generated.");
        currentFloorIndex++;
        // #TODO Maybe trigger something else and connect with game manager when and if implemented
    }
}