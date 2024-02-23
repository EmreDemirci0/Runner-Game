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
    /// Olmesi icin engele ne kadar yakin olmasi gerektigini ayarlatan uzaklik degeri (I��n�n uzunlu�u)
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
        // I��n ba�lang�� noktas� ve y�n� i�in de�i�kenler
        Vector3 rayOrigin;
        Vector3 rayDirection;

        // I��n� �izimi i�in hit de�i�keni
        RaycastHit hit;

        // �st oyuncu transformu i�in rayOrigin ve rayDirection ayarlamalar�
        rayOrigin = playerTopTransform.position;
        rayDirection = playerTopTransform.forward;

        // �ne do�ru ���n at���
        CastRay(rayOrigin, rayDirection, rayLength);

        // Sa�a do�ru ���n at���
        rayDirection = playerTopTransform.right;
        CastRay(rayOrigin, rayDirection, rayLength);

        // Sola do�ru ���n at���
        rayDirection = -playerTopTransform.right;
        CastRay(rayOrigin, rayDirection, rayLength);

        // Alt oyuncu transformu i�in rayOrigin ve rayDirection ayarlamalar�
        rayOrigin = playerBottomTransform.position;
        rayDirection = playerBottomTransform.forward;

        // �ne do�ru ���n at���
        CastRay(rayOrigin, rayDirection, rayLength);

        // Sa�a do�ru ���n at���
        rayDirection = playerBottomTransform.right;
        CastRay(rayOrigin, rayDirection, rayLength);

        // Sola do�ru ���n at���
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

        // I��n� �izdir ve bir nesne ile �arp���rsa bilgiyi hit de�i�kenine atar
        if (Physics.Raycast(origin, direction, out hit, maxDistance,layerMask))
        {
            // E�er ���n bir nesne ile �arp���rsa, o noktaya bir k�re �izdir
            Debug.DrawRay(origin, direction * hit.distance, Color.red);
            PlayerDead();
        }
        else
        {
            // E�er ���n bir nesne ile �arp��mazsa, ���n� uzunlu�u kadar �izdir
            Debug.DrawRay(origin, direction * maxDistance, Color.green);
        }
    }
  
    /// <summary>
    /// Player olunce calisan method
    /// </summary>
    private void PlayerDead()
    {
        print("�ld�n");
        Time.timeScale = 0.001f;
    }
    
    #endregion
}
