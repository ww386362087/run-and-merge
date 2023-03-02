using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchEventForAnimation : MonoBehaviour
{
    private Warrior warrior;
    private Monster monster;

    // Start is called before the first frame update
    void Start()
    {
        warrior = GetComponentInParent<Warrior>();
        monster = GetComponentInParent<Monster>();
    }

    void FireArrow()
    {
        warrior.fight_arrow();
    }

    void VFX()
    {
        monster.SpawnVfx();
    }
}
