using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace ECS_tutorial
{
    //TODO какое-то говно для фикса ошибки, нужно почитать
    public struct ZombieSpawnPoints: IComponentData
    {
        public BlobAssetReference<ZombieSpawnPointsPool> Blob;
       
    }
   
    public struct ZombieSpawnPointsPool
    {
        public BlobArray<Hobby> Value;
    }
    public struct Hobby
    {
        public float3 Point;
    }

    
}
