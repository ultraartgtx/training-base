using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndRotate : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabToSpawn;
    public int numberOfPrefabs = 1000;

    public  List<Transform> pool;
    
    public float spawnRadius = 5f;
    // Start is called before the first frame update
    void Awake()
    {
        pool = new List<Transform>();
        
            for (int i = 0; i < numberOfPrefabs; i++)
            {
                // Генерируем случайные координаты в пределах spawnRadius
                Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;

                // Создаем префаб в сгенерированных координатах
                pool.Add(Instantiate(prefabToSpawn, new Vector3(randomPosition.x, 0f, randomPosition.y), Quaternion.identity).transform);

            }
        
        
    }

}
