using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Removable : MonoBehaviour
{
    [SerializeField] Transform[] a;
    [SerializeField] Transform[] b;
    
    [ContextMenu("Reorder")]
    void reorder()
    {
        for(int i =0; i < a.Length; i++)
        {
            b[i].position = a[i].position;
            b[i].rotation = a[i].rotation;
            b[i].localScale = a[i].localScale;
        }
    }
}
