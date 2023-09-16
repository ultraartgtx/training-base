using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Rendering;

namespace ECS_tutorial.Scripts.ComponentsAndTags
{
    public readonly partial struct GraveyardAspect : IAspect
    {
        public readonly Entity Entity;

        public readonly RefRO<LocalTransform> _LocalTransform;
        
        private readonly RefRO<GraveyardProperties> _graveyardProperties;
        private readonly RefRW<GraveyardRandom> _graveyardRandom;

        public int NumberTombstonesToSpawn => _graveyardProperties.ValueRO.NumberTombstonesToSpawn;
        public Entity TombstonePrefab=> _graveyardProperties.ValueRO.TombstonePrefab;

        public LocalTransform GetRandomTombstoneTransform()
        {
            return new LocalTransform
            {
                Position = GetRandomPosition(),
                Rotation =quaternion.identity ,
                Scale = 1f
            };
        }

        private float3 GetRandomPosition()
        {
            float3 randomPosition;
            do
            {
                randomPosition = _graveyardRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner); 
            } while (math.distancesq(_LocalTransform.ValueRO.Position,randomPosition)<=BRAIN_SAFETY_RADIUS);
            
            return randomPosition;
        }

        private const float BRAIN_SAFETY_RADIUS = 100f;

        private float3 MinCorner => _LocalTransform.ValueRO.Position- HalfDimensions;

        private float3 MaxCorner => _LocalTransform.ValueRO.Position + HalfDimensions;

        private float3 HalfDimensions => new float3()
        {
           
            x = _graveyardProperties. ValueRO.FieldDimensions.x * 0.5f,
            y = 0f,
            z = _graveyardProperties. ValueRO.FieldDimensions.y * 0.5f
        };


    }
}