using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public Queue<GameObject> pooledObject;
    public GameObject objectPrefab;
    [SerializeField] public int poolSize;
    private void Awake()
    {
        pooledObject = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);
            pooledObject.Enqueue(obj);  
        }
    }
    public GameObject GetPooledObject()
    {
        GameObject obj=pooledObject.Dequeue();
        obj.SetActive(true);

        pooledObject.Enqueue(obj);
        return obj;
    }
}
