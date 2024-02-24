using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSetStart : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Sahnedeki itemlerin listeme atadigim engellerden gelmesini istedigim itemleri koyacagimiz kisim
    /// </summary>
    [SerializeField] private List<GameObject> items;
    #endregion 

    #region Methods
    /// <summary>
    /// Listeden cekilen engellerin rastgele bir sekilde active olma alani
    /// </summary>
    void Start()
    {
        foreach (GameObject item in items)
        {
            item.SetActive(false);
        }
        int random = Random.Range(0, items.Count);
        items[random].SetActive(true);
    }
    #endregion

}
