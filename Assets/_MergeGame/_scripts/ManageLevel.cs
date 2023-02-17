using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageLevel : MonoBehaviour
{
    public List<Monster> list_monster;
    public List<Warrior> list_warrior;

    Enemies enemies_script;

    private void Awake()
    {
        enemies_script = FindObjectOfType<Enemies>();
        
    }

    private void Start()
    {
        add_to_lists_enemies();
    }


    public void add_to_lists_enemies()
    {
        if(list_monster.Count != 0)
        {
            enemies_script.list_active_monsters = new List<Monster>(list_monster);
        }

        if(list_warrior.Count != 0)
        {
            enemies_script.list_active_warriors = new List<Warrior>(list_warrior);
        }
    }

    [ContextMenu("Populate List")]
    void PopulateList()
    {
        list_monster.Clear();
        list_warrior.Clear();

        foreach (Transform go in transform)
        {
            if (go.GetComponent<Monster>() != null)
            {
                list_monster.Add(go.GetComponent<Monster>());
            }
            else
            {
                list_warrior.Add(go.GetComponent<Warrior>());
            }
        }
    }
}
