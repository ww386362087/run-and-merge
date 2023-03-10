using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAP_RemoveAds : MonoBehaviour
{
    public void Buy_Success()
    {
        Module.remove_ads = 1;
        FirebaseManager.Instance.LogEvent_firebase_purchase();
    }
}
