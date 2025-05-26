using UnityEngine;
using System.Collections.Generic;
using ProceduralGeneration;
using ProceduralGeneration.ScriptableObjects;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [SerializeField] private FloorGenerator _floorGenerator;

    [SerializeField] private List<FloorConfig> floorConfigs;

    // #TODO [jn] when gamemanager is created than select based on it
    [SerializeField] private FloorGenerationStrategySO strategyAsset;
    private IFloorGenerationStrategy strategy = new StandardGenerationStrategy();

    [SerializeField] private int seed = -1; // -1 = random
    public int UsedSeed { get; private set; }

    public UnityEvent OnPlayerSpawnRequest = new UnityEvent();

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
        
        strategy = strategyAsset != null ? strategyAsset.GetStrategy() : new StandardGenerationStrategy();

        
        UsedSeed = (seed == -1) ? Random.Range(0, int.MaxValue) : seed;
        Random.InitState(UsedSeed);
        Debug.Log($"Seed used for generation: {UsedSeed}");

        _floorGenerator.OnFloorGenerated.AddListener(OnFloorGenerationComplete);
        GenerateNextFloor();
        
    }

    public void GenerateNextFloor()
    {
        if (!HasMoreFloors)
        {
            Debug.Log("All floors generated.");
            return;
        }

        FloorConfig config = floorConfigs[currentFloorIndex];
        _floorGenerator.GenerateFloor(config, strategy);
    }

    public void OnFloorGenerationComplete()
    {
        Debug.Log($"Floor {currentFloorIndex + 1} generated.");
        currentFloorIndex++;

        CameraManager.Instance.InitializeCameraList();
        CameraManager.Instance.AlignAllCamerasToReference();
        OnPlayerSpawnRequest.Invoke();
        // #TODO Maybe trigger something else and connect with game manager when and if implemented
    }
}