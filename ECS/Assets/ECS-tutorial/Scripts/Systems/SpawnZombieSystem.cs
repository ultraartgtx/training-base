using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Rendering;

namespace ECS_tutorial
{
    [BurstCompile]
    public partial struct SpawnZombieSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
           
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            new SpawnZombieJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton. CreateCommandBuffer (state. WorldUnmanaged)
            }. Run ();
        }
    }
    [BurstCompile]
    public partial struct SpawnZombieJob:IJobEntity
    {

        public float DeltaTime;
        public EntityCommandBuffer ECB;

        private void Execute(GraveyardAspect graveyard)
        {
            graveyard.ZombieSpawnTimer -= DeltaTime;
            if(!graveyard.TimeToSpawnZombie) return;
            graveyard.ZombieSpawnTimer = graveyard.ZombieSpawnRate;
            var newZombie = ECB.Instantiate(graveyard.ZombiePrefab);
        }
    }
}