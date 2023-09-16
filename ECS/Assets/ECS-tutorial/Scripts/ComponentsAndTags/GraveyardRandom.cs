using Unity.Entities;
using Unity.Mathematics;

namespace ECS_tutorial.Scripts.ComponentsAndTags
{
    public struct GraveyardRandom : IComponentData
    {
        public Random Value;
    }
}