using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCheck : MonoBehaviour
{
    [SerializeField] Transform playerTopTransform;
    [SerializeField] Transform playerBottomTransform;
    public float rayLength = 10f; // I��n�n uzunlu�u

    void Update()
    {
        // I��n ba�lang�� noktas� ve y�n� i�in de�i�kenler
        Vector3 rayOrigin;
        Vector3 rayDirection;

        // I��n� �izimi i�in hit de�i�keni
        RaycastHit hit;

        // �st oyuncu transformu i�in rayOrigin ve rayDirection ayarlamalar�
        rayOrigin = playerTopTransform.position;
        rayDirection = playerTopTransform.forward;

        // I��n� �izdir ve bir nesne ile �arp���rsa bilgiyi hit de�i�kenine atar
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayLength))
        {
            // E�er ���n bir nesne ile �arp���rsa, o noktaya bir k�re �izdir
            Debug.DrawRay(rayOrigin, rayDirection * hit.distance, Color.red);
            print("�ld�n");
            Time.timeScale = 0.001f;
            return; // �arp��ma alg�land�ysa di�er ray kontrol�ne gerek yok
        }
        else
        {
            // E�er ���n bir nesne ile �arp��mazsa, ���n� uzunlu�u kadar �izdir
            Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.green);
        }

        // Alt oyuncu transformu i�in rayOrigin ve rayDirection ayarlamalar�
        rayOrigin = playerBottomTransform.position;
        rayDirection = playerBottomTransform.forward;

        // I��n� �izdir ve bir nesne ile �arp���rsa bilgiyi hit de�i�kenine atar
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayLength))
        {
            // E�er ���n bir nesne ile �arp���rsa, o noktaya bir k�re �izdir
            Debug.DrawRay(rayOrigin, rayDirection * hit.distance, Color.red);
            print("�ld�n");
            Time.timeScale = 0.001f;
        }
        else
        {
            // E�er ���n bir nesne ile �arp��mazsa, ���n� uzunlu�u kadar �izdir
            Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.green);
        }
    }

}
