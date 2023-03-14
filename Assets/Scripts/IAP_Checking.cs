using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAP_Checking : MonoBehaviour
{
    void OnEnable()
    {
        gameObject.SetActive(Module.remove_ads != 1);
    }
}
