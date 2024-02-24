using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCheck : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// Playerin ust kismini tutan transform
    /// </summary>
    [SerializeField] Transform playerTopTransform;

    /// <summary>
    /// Playerin alt kismini tutan transform
    /// </summary>
    [SerializeField] Transform playerBottomTransform;

    /// <summary>
    /// Olmesi icin engele ne kadar yakin olmasi gerektigini ayarlatan uzaklik degeri (Isinin uzunlugu)
    /// </summary>
    public float rayLength = .10f;

    /// <summary>
    /// Olmesi icin engel layeri
    /// </summary>
    public LayerMask layerMask;
    #endregion


    #region Methods

    /// <summary>
    /// Olum kontrolu ve islemleri yapan Update methodu
    /// </summary>
    void Update()
    {
        RayCastOperations();
    }
   
    /// <summary>
    /// Karakterin onune,sag ve soluna ray atip olup olmediginin kontrolunu yapan method
    /// </summary>
    void RayCastOperations()
    {
        Vector3 rayOrigin;
        Vector3 rayDirection;

        RaycastHit hit;

        rayOrigin = playerTopTransform.position;
        rayDirection = playerTopTransform.forward;

        CastRay(rayOrigin, rayDirection, rayLength);

        rayDirection = playerTopTransform.right;
        CastRay(rayOrigin, rayDirection, rayLength);

        rayDirection = -playerTopTransform.right;
        CastRay(rayOrigin, rayDirection, rayLength);

        rayOrigin = playerBottomTransform.position;
        rayDirection = playerBottomTransform.forward;

        CastRay(rayOrigin, rayDirection, rayLength);

        rayDirection = playerBottomTransform.right;
        CastRay(rayOrigin, rayDirection, rayLength);

        rayDirection = -playerBottomTransform.right;
        CastRay(rayOrigin, rayDirection, rayLength);
    }
    
    /// <summary>
    /// Ray Control methodu
    /// </summary>
    /// <param name="origin"> Baslangic Noktasi </param>
    /// <param name="direction"> Yonu </param>
    /// <param name="maxDistance"> Uzakligi </param>
    void CastRay(Vector3 origin, Vector3 direction, float maxDistance)
    {
        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, maxDistance,layerMask))
        {
            Debug.DrawRay(origin, direction * hit.distance, Color.red);
            PlayerDead();
        }
        else
        {
            Debug.DrawRay(origin, direction * maxDistance, Color.green);
        }
    }
  
    /// <summary>
    /// Player olunce calisan method
    /// </summary>
    private void PlayerDead()
    {
        GameHandler.Instance.isDead = true;
    }
    
    #endregion
}
