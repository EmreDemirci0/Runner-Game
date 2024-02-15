using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Singleton
    /// </summary>
    public static ObjectPool Instance { get; private set; }

    /// <summary>
    /// Havuzdaki objelerin tutuldugu veri tipi (Kuyruk)
    /// </summary>
    public Queue<GameObject> pooledObject;

    /// <summary>
    /// Havuza atýlacak objele ornegi
    /// </summary>
    public GameObject objectPrefab;

    /// <summary>
    /// Kac tane item olusacagi kismi
    /// </summary>
    [SerializeField] public int poolSize;
    #endregion

    #region Methods

    /// <summary>
    /// Singleton ve Object pool atamalari yapilan Awake methodu
    /// </summary>
    private void Awake()
    {
        #region Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        #endregion

        //Object Pool iþlemleri
        pooledObject = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);
            pooledObject.Enqueue(obj);  
        }
    }

    /// <summary>
    /// Object pooldan obje cekilen method
    /// </summary>
    public GameObject GetPooledObject()
    {
        GameObject obj=pooledObject.Dequeue();
        obj.SetActive(true);

        pooledObject.Enqueue(obj);
        return obj;
    }
    #endregion
}
