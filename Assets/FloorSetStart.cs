using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSetStart : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Sahnedeki itemlerin listeme atadýðým engellerden gelmesini istediðim itemleri koyacagimiz kisim
    /// </summary>
    [SerializeField] private List<GameObject> items;
    #endregion 

    #region Methods
    /// <summary>
    /// Listeden cekilen engellerin rastgele bir sekilde active olma alaný
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
