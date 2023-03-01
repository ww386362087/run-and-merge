using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePositionRig : MonoBehaviour
{
    
    [ContextMenu("Change position")]
    void MoveNode()
    {
        var newObj = new GameObject("Root_Back");
        newObj.transform.SetParent(transform);
        newObj.transform.localPosition = Vector3.zero;

        var rootBack = transform.Find("back_Root_M");
        var root = transform.Find("root").GetChild(0);
        newObj.transform.SetParent(root);
        rootBack.SetParent(newObj.transform);
    }

}
