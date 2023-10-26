using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Rendering;

namespace ECS_tutorial
{
   [BurstCompile]
   [UpdateInGroup(typeof(InitializationSystemGroup))]
   public partial struct SpawnTombstoneSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
           state.RequireForUpdate<GraveyardProperties>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            var graveyardEntity = SystemAPI.GetSingletonEntity<GraveyardProperties>();
            var graveyard = SystemAPI.GetAspect<GraveyardAspect>(graveyardEntity);

            var ecb = new EntityCommandBuffer(Allocator.Temp);
            //ref var spawnPoints = ref new BlobArray<float3>(Allocator.Temp);
            var builder = new BlobBuilder(Allocator.Temp);
            ref ZombieSpawnPointsPool zombieSpawnPointsPool = ref builder.ConstructRoot<ZombieSpawnPointsPool>();
            
            int numHobbies = graveyard.NumberTombstonesToSpawn;
            
            BlobBuilderArray<Hobby> arrayBuilder = builder.Allocate(
                ref zombieSpawnPointsPool.Value,
                numHobbies
            );
                    
            
            var tombstoneOffset = new float3(0f, -2f, 1f);
            for (var i = 0; i < graveyard.NumberTombstonesToSpawn; i++)
            {
                var newTombstone = ecb.Instantiate(graveyard.TombstonePrefab);
                var newTombstoneTransform = graveyard.GetRandomTombstoneTransform();
                ecb.SetComponent(newTombstone,newTombstoneTransform);
                
                var newZombieSpawnPoint = newTombstoneTransform.Position + tombstoneOffset;
                arrayBuilder[i].Point = newZombieSpawnPoint;
            }

            
                //spawnPoints.ToArray(Allocator.Persistent);
            var result = builder.CreateBlobAssetReference<ZombieSpawnPointsPool>(Allocator.Persistent);
            builder.Dispose();
            
            graveyard.ZombieSpawnPoints = result;
            ecb.Playback(state.EntityManager);
        }
    }
}