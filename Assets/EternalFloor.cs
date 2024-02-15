using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EternalFloor : MonoBehaviour
{
    [SerializeField] float spawnInterval = 3;
    [SerializeField] ObjectPool objectPool = null;
    private float objectZvalue;

    private void Start()
    {
        objectZvalue = objectPool.objectPrefab.transform.localScale.z;
        SpawnInitialObjects(); // Tüm yollarý baþlangýçta yerleþtir
        Invoke("WaitForSpawnInitialObjects",spawnInterval);
    }
    void WaitForSpawnInitialObjects()
    {
        StartCoroutine(SpawnObjects());
    }
    void SpawnInitialObjects()
    {
        float totalZOffset = 0;

        for (int i = 0; i < objectPool.poolSize; i++)
        {
            GameObject obj = objectPool.GetPooledObject();
            obj.transform.position = new Vector3(0, 0, totalZOffset);
            totalZOffset += objectZvalue * 5;
        }
    }
    IEnumerator SpawnObjects()
    {
        float totalZOffset = objectPool.poolSize * objectZvalue * 5; // Baþlangýçta oluþturulan objelerin sonundan devam et
        GameObject lastObject = null;

        while (true)
        {
            GameObject obj = objectPool.GetPooledObject();
            obj.transform.position = new Vector3(0, 0, totalZOffset);
            totalZOffset += objectZvalue * 5;

            // Karakterin geçtiði objenin en sona gitmesi için kontrol
            if (lastObject != null && transform.position.z > lastObject.transform.position.z)
            {
                // Son objeyi alarak yeni pozisyona yerleþtir
                lastObject.transform.position = new Vector3(0, 0, totalZOffset);
                totalZOffset += objectZvalue * 5;
            }

            // Son objeyi güncelle
            lastObject = obj;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
