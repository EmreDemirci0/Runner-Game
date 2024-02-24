using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EternalFloor : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// Singleton
    /// </summary>
    public static EternalFloor Instance { get; private set; }

    /// <summary>
    /// Object poolun kac saniyede bir kendini tekrarladigini tutan degisken, PlayerMovement icerisindeki moveSpeed ve buradaki objectZvalue degeri ile formul olusturulup, cikan sonuc ilgili degerimiz oluyor.
    /// </summary>
    float spawnInterval;

    /// <summary>
    /// Zeminimin Z ekseninde boyutu
    /// </summary>
    float objectZvalue;
    #endregion

    #region Methods

    /// <summary>
    /// Singleton atamalari yapilan kisim
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    /// <summary>
    /// Zemin prefabinin Z scale'i, spawnInterval degerini formulle olusturan method, baslangicta zeminleri atayan method ve sirali olarak object pool objelerini ayarlayan method.
    /// </summary>
    private void Start()
    {
        objectZvalue = ObjectPool.Instance.objectPrefab.transform.GetChild(0).localScale.z;
        UpdateSpawnInternalValue();
        SpawnInitialObjects();  
        Invoke("WaitForSpawnInitialObjects", spawnInterval);
    }

    private void Update()
    {
        UpdateSpawnInternalValue();
    }
    /// <summary>
    /// Baslangicta hemen itemleri yuklemesin, bir partiyi start kisminda default ekledigimiz icin biraz delayli bir sekilde object pool ayarini yapan method
    /// </summary>
    void WaitForSpawnInitialObjects()
    {
        StartCoroutine(SpawnObjects());
    }

    /// <summary>
    /// SpawnInterval degerini PlayerMovement.Instance.moveSpeed ve objectZvalue ile atanan method
    /// </summary>
    public void UpdateSpawnInternalValue()
    {
        //Bu Formul Objectpooldeki objelerin zeminin boyutuna ve hizimiza göre ayarlanmasýný saglar.
        spawnInterval = 5.250f / PlayerMovement.Instance.moveSpeed * objectZvalue; //6->28   5.5 44 5.25 84
    }
    
    /// <summary>
    /// Start icin baslangictaki bir parti itemi getiren method
    /// </summary>
    void SpawnInitialObjects()
    {
        float totalZOffset =0;

        for (int i = 0; i < ObjectPool.Instance.poolSize; i++)
        {
            GameObject obj = ObjectPool.Instance.GetPooledObject();
            obj.transform.position = new Vector3(0, 0, totalZOffset);
            totalZOffset += objectZvalue * 5;
        }
    }

    /// <summary>
    ///  Object Pool icin Ayarlamalarin yapilan method
    /// </summary>
    IEnumerator SpawnObjects()
    {
        
       
        float totalZOffset = ObjectPool.Instance.poolSize * objectZvalue * 5; 
        GameObject lastObject = null;

        while (true)
        {

            if (PlayerMovement.Instance.moveSpeed <=20 )
            {
                PlayerMovement.Instance.moveSpeed += .5f;
                UpdateSpawnInternalValue();
            }
           
            GameObject obj = ObjectPool.Instance.GetPooledObject();
            obj.transform.position = new Vector3(0, 0, totalZOffset);
            totalZOffset += objectZvalue * 5;

          
            if (lastObject != null && transform.position.z > lastObject.transform.position.z)
            {
           
                lastObject.transform.position = new Vector3(0, 0, totalZOffset);
                totalZOffset += objectZvalue * 5;
            }

       
            lastObject = obj;

            yield return new WaitForSeconds(spawnInterval);
        }

    }
  
    #endregion

}
