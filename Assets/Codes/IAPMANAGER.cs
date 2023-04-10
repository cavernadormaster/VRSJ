using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPMANAGER : MonoBehaviour
{
    public string FlashLight = "com.osRenegados.VRTest.FlashLight";
    // Start is called before the first frame update
   public void OnPurchaseComplete(Product product)
    {
        Debug.Log("Flash");
    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("your purchase failed beacuse:" + reason);
    }
}
