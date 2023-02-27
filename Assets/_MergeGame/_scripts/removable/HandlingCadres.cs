using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlingCadres : MonoBehaviour
{
    [SerializeField] List<Transform> holders;
    [SerializeField] List<Transform> cadres;

    public void RemoveCadres()
    {
        foreach (Transform trans in cadres)
        {
            Destroy(trans.gameObject);
        }

        cadres.Clear();
    }

    public void SetCadres(List<Transform> list)
    {
        cadres = list;
        SetParent();
    }

    [ContextMenu("SetParent")]
    public void SetParent()
    {
        for (int i = 0; i < cadres.Count; i++)
        {
            cadres[i].SetParent(holders[i]);
            cadres[i].localPosition = Vector3.zero;
        }
    }
}
