using UnityEngine;

namespace ProceduralGeneration.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Floor Generation/Easy Bias Strategy")]
    public class EasyStrategySO : FloorGenerationStrategySO
    {
        public override IFloorGenerationStrategy GetStrategy() => new EasyGenerationStrategy();
    }

}