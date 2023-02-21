using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSetInit : MonoBehaviour
{
    void OnEnable()
    {
        if (GameSceneLoad.Instance != null)
        {
            GameSceneLoad.Instance.mainCam = this.GetComponent<Camera>();
            this.transform.parent = GameSceneLoad.Instance.transform;
        }
           
    }
}
