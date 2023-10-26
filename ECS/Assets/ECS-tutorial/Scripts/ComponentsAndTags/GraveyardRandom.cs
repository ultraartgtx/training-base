using Unity.Entities;
using Unity.Mathematics;

namespace ECS_tutorial
{
    public struct GraveyardRandom : IComponentData
    {
        public Random Value;
    }
}