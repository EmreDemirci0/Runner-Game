using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCheck : MonoBehaviour
{
    [SerializeField] Transform playerTopTransform;
    [SerializeField] Transform playerBottomTransform;
    public float rayLength = 10f; // Iþýnýn uzunluðu

    void Update()
    {
        // Iþýn baþlangýç noktasý ve yönü için deðiþkenler
        Vector3 rayOrigin;
        Vector3 rayDirection;

        // Iþýný çizimi için hit deðiþkeni
        RaycastHit hit;

        // Üst oyuncu transformu için rayOrigin ve rayDirection ayarlamalarý
        rayOrigin = playerTopTransform.position;
        rayDirection = playerTopTransform.forward;

        // Iþýný çizdir ve bir nesne ile çarpýþýrsa bilgiyi hit deðiþkenine atar
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayLength))
        {
            // Eðer ýþýn bir nesne ile çarpýþýrsa, o noktaya bir küre çizdir
            Debug.DrawRay(rayOrigin, rayDirection * hit.distance, Color.red);
            print("Öldün");
            Time.timeScale = 0.001f;
            return; // Çarpýþma algýlandýysa diðer ray kontrolüne gerek yok
        }
        else
        {
            // Eðer ýþýn bir nesne ile çarpýþmazsa, ýþýný uzunluðu kadar çizdir
            Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.green);
        }

        // Alt oyuncu transformu için rayOrigin ve rayDirection ayarlamalarý
        rayOrigin = playerBottomTransform.position;
        rayDirection = playerBottomTransform.forward;

        // Iþýný çizdir ve bir nesne ile çarpýþýrsa bilgiyi hit deðiþkenine atar
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayLength))
        {
            // Eðer ýþýn bir nesne ile çarpýþýrsa, o noktaya bir küre çizdir
            Debug.DrawRay(rayOrigin, rayDirection * hit.distance, Color.red);
            print("Öldün");
            Time.timeScale = 0.001f;
        }
        else
        {
            // Eðer ýþýn bir nesne ile çarpýþmazsa, ýþýný uzunluðu kadar çizdir
            Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.green);
        }
    }

}
