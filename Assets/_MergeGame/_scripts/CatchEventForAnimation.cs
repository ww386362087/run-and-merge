using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchEventForAnimation : MonoBehaviour
{
    private Warrior warrior;

    // Start is called before the first frame update
    void Start()
    {
        warrior = GetComponentInParent<Warrior>();
    }

    void FireArrow()
    {
        warrior.fight_arrow();
    }
}
