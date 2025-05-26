using UnityEngine;

namespace ProceduralGeneration.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Floor Generation/Hard Bias Strategy")]
    public class HardStrategySO : FloorGenerationStrategySO
    {
        public override IFloorGenerationStrategy GetStrategy() => new HardGenerationStrategy();
    }

}