using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using UnityEngine;

public class AddArrow : MonoBehaviour
{
    [SerializeField] Warrior[] warriors;
    [SerializeField] GameObject newArrow;

    [ContextMenu("Change model arrow")]
    void UpdateArrow()
    {
        foreach (var w in warriors)
        {
            var l = new List<GameObject>();
            for(int i  =0; i < 3; i++)
            {
                var n = PrefabUtility.InstantiatePrefab(newArrow, w.transform) as GameObject;
                n.AddComponent<Weapon>();
                l.Add(n);
            }

            w.arrows = l.ToArray();
        }
    }


    [ContextMenu("RemoveArrow")]
    void RemoveArrow()
    {
        for(int i =0;i < transform.childCount; i++)
        {
            var c = transform.GetChild(i);
            if (c.transform.name.Contains("Arrow"))
            {
                DestroyImmediate(c.gameObject, true);
            }
            if (c.transform.name.Contains("Cyborg projectile"))
                c.gameObject.SetActive(false);
            
        }
    }
}
