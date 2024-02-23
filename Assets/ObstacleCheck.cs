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
    /// Olmesi icin engele ne kadar yakin olmasi gerektigini ayarlatan uzaklik degeri (Iþýnýn uzunluðu)
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
        // Iþýn baþlangýç noktasý ve yönü için deðiþkenler
        Vector3 rayOrigin;
        Vector3 rayDirection;

        // Iþýný çizimi için hit deðiþkeni
        RaycastHit hit;

        // Üst oyuncu transformu için rayOrigin ve rayDirection ayarlamalarý
        rayOrigin = playerTopTransform.position;
        rayDirection = playerTopTransform.forward;

        // Öne doðru ýþýn atýþý
        CastRay(rayOrigin, rayDirection, rayLength);

        // Saða doðru ýþýn atýþý
        rayDirection = playerTopTransform.right;
        CastRay(rayOrigin, rayDirection, rayLength);

        // Sola doðru ýþýn atýþý
        rayDirection = -playerTopTransform.right;
        CastRay(rayOrigin, rayDirection, rayLength);

        // Alt oyuncu transformu için rayOrigin ve rayDirection ayarlamalarý
        rayOrigin = playerBottomTransform.position;
        rayDirection = playerBottomTransform.forward;

        // Öne doðru ýþýn atýþý
        CastRay(rayOrigin, rayDirection, rayLength);

        // Saða doðru ýþýn atýþý
        rayDirection = playerBottomTransform.right;
        CastRay(rayOrigin, rayDirection, rayLength);

        // Sola doðru ýþýn atýþý
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

        // Iþýný çizdir ve bir nesne ile çarpýþýrsa bilgiyi hit deðiþkenine atar
        if (Physics.Raycast(origin, direction, out hit, maxDistance,layerMask))
        {
            // Eðer ýþýn bir nesne ile çarpýþýrsa, o noktaya bir küre çizdir
            Debug.DrawRay(origin, direction * hit.distance, Color.red);
            PlayerDead();
        }
        else
        {
            // Eðer ýþýn bir nesne ile çarpýþmazsa, ýþýný uzunluðu kadar çizdir
            Debug.DrawRay(origin, direction * maxDistance, Color.green);
        }
    }
  
    /// <summary>
    /// Player olunce calisan method
    /// </summary>
    private void PlayerDead()
    {
        print("Öldün");
        Time.timeScale = 0.001f;
    }
    
    #endregion
}
