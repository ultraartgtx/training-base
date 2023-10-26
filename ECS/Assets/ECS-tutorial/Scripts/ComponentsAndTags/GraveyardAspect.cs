using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Rendering;

namespace ECS_tutorial
{
    public readonly partial struct GraveyardAspect : IAspect
    {
        public readonly Entity Entity;

        public readonly RefRO<LocalTransform> _LocalTransform;
        
        private readonly RefRO<GraveyardProperties> _graveyardProperties;
        private readonly RefRW<GraveyardRandom> _graveyardRandom;
        private readonly RefRW<ZombieSpawnPoints> _zombieSpawnPoints;
        private readonly RefRW<ZombieSpawnTimer> _zombieSpawnTimer;
        public int NumberTombstonesToSpawn => _graveyardProperties.ValueRO.NumberTombstonesToSpawn;
        public Entity TombstonePrefab=> _graveyardProperties.ValueRO.TombstonePrefab;

        public BlobAssetReference<ZombieSpawnPointsPool> ZombieSpawnPoints
        {
            get => _zombieSpawnPoints.ValueRO.Blob;
            set => _zombieSpawnPoints.ValueRW.Blob = value;
        }

        public LocalTransform GetRandomTombstoneTransform()
        {
            return new LocalTransform
            {
                Position = GetRandomPosition(),
                Rotation =GetRandomRotation(),
                Scale = GetRandomScale(0.5f)
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
        
        private float3 MinCorner => _LocalTransform.ValueRO.Position- HalfDimensions;

        private float3 MaxCorner => _LocalTransform.ValueRO.Position + HalfDimensions;

        private float3 HalfDimensions => new float3()
        {
           
            x = _graveyardProperties. ValueRO.FieldDimensions.x * 0.5f,
            y = 0f,
            z = _graveyardProperties. ValueRO.FieldDimensions.y * 0.5f
        };
        private const float BRAIN_SAFETY_RADIUS = 100f;

        private quaternion GetRandomRotation() =>
            quaternion.RotateY(_graveyardRandom.ValueRW.Value.NextFloat(-0.25f, 0.25f));

        private float GetRandomScale(float min) => _graveyardRandom.ValueRW.Value.NextFloat(min, 1f);

        public float2 GetRandomOffset()
        {
            return _graveyardRandom.ValueRW.Value.NextFloat2();
        }

        public float ZombieSpawnTimer
        {
            get => _zombieSpawnTimer.ValueRO.Value;
            set => _zombieSpawnTimer.ValueRW.Value = value;
        }

        public bool TimeToSpawnZombie => ZombieSpawnTimer <= 0f;

        public float ZombieSpawnRate => _graveyardProperties.ValueRO.ZombieSpawnRate;
        public Entity ZombiePrefab => _graveyardProperties.ValueRO.ZombiePrefab;


    }
}