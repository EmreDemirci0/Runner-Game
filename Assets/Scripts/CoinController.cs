using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    /// <summary>
    /// Coinlerin her icine girince ilgili islemler yapilir.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
            GameHandler.Instance.AddScore();

            Invoke("Open10Sec", 10);
        }
    }

    
    /// <summary>
    /// Coin 10 sn sonra tekrar setactive(true) yapilan method.
    /// </summary>
    void Open10Sec()
    {
        this.gameObject.SetActive(true);
    }

}
