using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyGameobject : Singleton<DontDestroyGameobject>
{
    protected override void Awake()
    {
        base.Awake();
        if (transform.childCount > 0)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }

}
