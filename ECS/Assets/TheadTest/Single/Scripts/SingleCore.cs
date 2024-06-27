using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCore : MonoBehaviour
{
    public SpawnAndRotate _poolClass;
    public float rotationSpeed;
    public int loadCount=100 ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < loadCount; i++)
        {
            foreach (var obj in _poolClass.pool)
            {
                obj.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
        }
        
    }
}
