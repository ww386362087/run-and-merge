using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingAdsOffer : MonoBehaviour
{
     void OnEnable()
    {
        FirebaseManager.Instance.LogEvent_firebase_ads_reward_offer();
    }
}
