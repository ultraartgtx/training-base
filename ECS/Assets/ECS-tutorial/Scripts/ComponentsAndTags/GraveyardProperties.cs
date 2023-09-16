using Unity.Entities;
using Unity.Mathematics;

namespace ECS_tutorial.Scripts.ComponentsAndTags
{
    public struct GraveyardProperties: IComponentData
    {
        public float2 FieldDimensions;
        public int NumberTombstonesToSpawn;
        public Entity TombstonePrefab;
    }
}