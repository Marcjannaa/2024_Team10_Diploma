namespace ProceduralGeneration.ScriptableObjects
{
    using UnityEngine;

    public abstract class FloorGenerationStrategySO : ScriptableObject
    {
        public abstract IFloorGenerationStrategy GetStrategy();
    }

}