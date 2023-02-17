﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum monster_type_enum
{
    none,
    level_1,
    level_2,
    level_3,
    level_4,
    level_5,
    level_6,
    level_7,
    level_8,
    level_9,
    level_10
}

public enum warrior_type_enum
{
    none,
    level_1,
    level_2,
    level_3,
    level_4,
    level_5,
    level_6,
    level_7,
    level_8,
    level_9,
    level_10
}

public class Cadre : MonoBehaviour
{
    public Transform pos;
    public bool has_din, has_warrior , active;
    public monster_type_enum monster_type;
    public warrior_type_enum warrior_type;
    public List<GameObject> list_dinosaurs;
    public List<GameObject> list_warriors;
    public GameObject dinosaur_parent;
    public GameObject warriors_parent;
    public Monster active_monster;
    public Warrior active_warrior;
    Players Players_script;

    public GameObject pref , pref2;
    public ParticleSystem effect_one , effect_two;
    //public GameObject pref;

    // Start is called before the first frame update
    void Start()
    {
        Players_script = FindObjectOfType<Players>();

        //Destroy(dinosaur_parent);
        //Destroy(warriors_parent);

        //GameObject gm = Instantiate(pref, pos.position, pref.transform.rotation, transform);
        //gm.name = "all_warrior";

        //GameObject gm2 = Instantiate(pref2, pos.position, pref.transform.rotation, transform);
        //gm2.name = "all_monster";

        //warriors_parent = gm;
        //dinosaur_parent = gm2;

        //list_warriors.Clear();
        //list_dinosaurs.Clear();

        //for (int i = 0; i < warriors_parent.transform.childCount; i++)
        //{
        //    list_warriors.Add(warriors_parent.transform.GetChild(i).gameObject);

        //}

        //for (int i = 0; i < dinosaur_parent.transform.childCount; i++)
        //{
        //    list_dinosaurs.Add(dinosaur_parent.transform.GetChild(i).gameObject);

        //}


        //for (int i = 0; i < list_dinosaurs.Count; i++)
        //{
        //    list_dinosaurs[i].SetActive(false);
        //}
        //monster_type = monster_type_enum.none;
        //has_din = false;

        // navmesh agent

        // effects
        //ParticleSystem ps_1 = Instantiate(effect_one, pos.position, effect_one.transform.rotation, transform);
        //ParticleSystem ps_2 = Instantiate(effect_two, pos.position, effect_two.transform.rotation, transform);

        //effect_one = ps_1;
        //effect_two = ps_2;

    }



    public void color_cadre()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    public void hide_color_cadre()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    // monster 
    public void set_true_din()
    {
        has_din = true;
    }

    public void set_false_din()
    {
        has_din = false;
    }

    public void set_active_true()
    {
        active = true;
    }

    public void set_active_false()
    {
        active = false;
    }

    public GameObject select_object_to_move()
    {
        return dinosaur_parent;
    }
    public void hide_all_monster()
    {
        for (int i = 0; i < list_dinosaurs.Count; i++)
        {
            list_dinosaurs[i].SetActive(false);
        }
    }

    public void add_monster()
    {
        has_din = true;
        list_dinosaurs[0].SetActive(true);
        active_monster = list_dinosaurs[0].GetComponent<Monster>();
        monster_type = monster_type_enum.level_1;

        //add to list players
        Players_script.list_active_monsters.Add(active_monster);

    }
    public void active_by_type_monster( monster_type_enum tp)
    {
        if(tp == monster_type_enum.level_1)
        {
            list_dinosaurs[0].SetActive(true);
            active_monster = list_dinosaurs[0].GetComponent<Monster>();
        }
        else if (tp == monster_type_enum.level_2)
        {
            list_dinosaurs[1].SetActive(true);
            active_monster = list_dinosaurs[1].GetComponent<Monster>();
        }
        else if (tp == monster_type_enum.level_3)
        {
            list_dinosaurs[2].SetActive(true);
            active_monster = list_dinosaurs[2].GetComponent<Monster>();
        }
        else if (tp == monster_type_enum.level_4)
        {
            list_dinosaurs[3].SetActive(true);
            active_monster = list_dinosaurs[3].GetComponent<Monster>();
        }
        else if (tp == monster_type_enum.level_5)
        {
            list_dinosaurs[4].SetActive(true);
            active_monster = list_dinosaurs[4].GetComponent<Monster>();
        }
        else if (tp == monster_type_enum.level_6)
        {
            list_dinosaurs[5].SetActive(true);
            active_monster = list_dinosaurs[5].GetComponent<Monster>();
        }
        else if (tp == monster_type_enum.level_7)
        {
            list_dinosaurs[6].SetActive(true);
            active_monster = list_dinosaurs[6].GetComponent<Monster>();
        }
        else if (tp == monster_type_enum.level_8)
        {
            list_dinosaurs[7].SetActive(true);
            active_monster = list_dinosaurs[7].GetComponent<Monster>();
        }
        else if (tp == monster_type_enum.level_9)
        {
            list_dinosaurs[8].SetActive(true);
            active_monster = list_dinosaurs[8].GetComponent<Monster>();
        }
        else if (tp == monster_type_enum.level_10)
        {
            list_dinosaurs[9].SetActive(true);
            active_monster = list_dinosaurs[9].GetComponent<Monster>();
        }
        
    }

    public void upgrate_level_and_active__monster(monster_type_enum tp)
    {
        if (tp == monster_type_enum.level_1)
        {
            hide_all_monster();
            list_dinosaurs[1].SetActive(true);
            active_monster = list_dinosaurs[1].GetComponent<Monster>();
            monster_type = monster_type_enum.level_2;

            // show panel new character monster
            if (GameManager.instance.get_count_active_monster() == 1)
            {
                GameManager.instance.set_count_active_monster(GameManager.instance.get_count_active_monster() + 1);
                UiManager.instance.show_panel_new_character_monster();
            }
        }
        else if (tp == monster_type_enum.level_2)
        {
            hide_all_monster();
            list_dinosaurs[2].SetActive(true);
            active_monster = list_dinosaurs[2].GetComponent<Monster>();
            monster_type = monster_type_enum.level_3;

            // show panel new character monster
            if (GameManager.instance.get_count_active_monster() == 2)
            {
                GameManager.instance.set_count_active_monster(GameManager.instance.get_count_active_monster() + 1);
                UiManager.instance.show_panel_new_character_monster();
            }
        }
        else if (tp == monster_type_enum.level_3)
        {
            hide_all_monster();
            list_dinosaurs[3].SetActive(true);
            active_monster = list_dinosaurs[3].GetComponent<Monster>();
            monster_type = monster_type_enum.level_4;

            // show panel new character monster
            if (GameManager.instance.get_count_active_monster() == 3)
            {
                GameManager.instance.set_count_active_monster(GameManager.instance.get_count_active_monster() + 1);
                UiManager.instance.show_panel_new_character_monster();
            }
        }
        else if (tp == monster_type_enum.level_4)
        {
            hide_all_monster();
            list_dinosaurs[4].SetActive(true);
            active_monster = list_dinosaurs[4].GetComponent<Monster>();
            monster_type = monster_type_enum.level_5;

            // show panel new character monster
            if (GameManager.instance.get_count_active_monster() == 4)
            {
                GameManager.instance.set_count_active_monster(GameManager.instance.get_count_active_monster() + 1);
                UiManager.instance.show_panel_new_character_monster();
            }
        }
        else if (tp == monster_type_enum.level_5)
        {
            hide_all_monster();
            list_dinosaurs[5].SetActive(true);
            active_monster = list_dinosaurs[5].GetComponent<Monster>();
            monster_type = monster_type_enum.level_6;

            // show panel new character monster
            if (GameManager.instance.get_count_active_monster() == 5)
            {
                GameManager.instance.set_count_active_monster(GameManager.instance.get_count_active_monster() + 1);
                UiManager.instance.show_panel_new_character_monster();
            }

        }
        else if (tp == monster_type_enum.level_6)
        {
            hide_all_monster();
            list_dinosaurs[6].SetActive(true);
            active_monster = list_dinosaurs[6].GetComponent<Monster>();
            monster_type = monster_type_enum.level_7;

            // show panel new character monster
            if (GameManager.instance.get_count_active_monster() == 6)
            {
                GameManager.instance.set_count_active_monster(GameManager.instance.get_count_active_monster() + 1);
                UiManager.instance.show_panel_new_character_monster();
            }
        }
        else if (tp == monster_type_enum.level_7)
        {
            hide_all_monster();
            list_dinosaurs[7].SetActive(true);
            active_monster = list_dinosaurs[7].GetComponent<Monster>();
            monster_type = monster_type_enum.level_8;

            // show panel new character monster
            if (GameManager.instance.get_count_active_monster() == 7)
            {
                GameManager.instance.set_count_active_monster(GameManager.instance.get_count_active_monster() + 1);
                UiManager.instance.show_panel_new_character_monster();
            }
        }
        else if (tp == monster_type_enum.level_8)
        {
            hide_all_monster();
            list_dinosaurs[8].SetActive(true);
            active_monster = list_dinosaurs[8].GetComponent<Monster>();
            monster_type = monster_type_enum.level_9;

            // show panel new character monster
            if (GameManager.instance.get_count_active_monster() == 8)
            {
                GameManager.instance.set_count_active_monster(GameManager.instance.get_count_active_monster() + 1);
                UiManager.instance.show_panel_new_character_monster();
            }
        }
        else if (tp == monster_type_enum.level_9)
        {
            hide_all_monster();
            list_dinosaurs[9].SetActive(true);
            active_monster = list_dinosaurs[9].GetComponent<Monster>();
            monster_type = monster_type_enum.level_10;

            // show panel new character monster
            if (GameManager.instance.get_count_active_monster() == 9)
            {
                GameManager.instance.set_count_active_monster(GameManager.instance.get_count_active_monster() + 1);
                UiManager.instance.show_panel_new_character_monster();
            }
        }
        else 
        {
            // no more upgrate 
            print("end upgrate");
        }
    }

    // warriors

    public void set_true_warrior()
    {
        has_warrior = true;
    }

    public void set_false_warrior()
    {
        has_warrior = false;
    }

    public GameObject select_object_to_move_warrior()
    {
        return warriors_parent;
    }

    public void hide_all_warriors()
    {
        for (int i = 0; i < list_warriors.Count; i++)
        {
            list_warriors[i].SetActive(false);
        }
    }

    public void add_warrior()
    {
        has_warrior = true;
        list_warriors[0].SetActive(true);
        warrior_type = warrior_type_enum.level_1;
        active_warrior = list_warriors[0].GetComponent<Warrior>();

        //add to list players
        Players_script.list_active_warriors.Add(active_warrior);
    }

    public void active_by_type_warrior(warrior_type_enum tp)
    {
        if (tp == warrior_type_enum.level_1)
        {
            list_warriors[0].SetActive(true);
            active_warrior = list_warriors[0].GetComponent<Warrior>();
        }
        else if (tp == warrior_type_enum.level_2)
        {
            list_warriors[1].SetActive(true);
            active_warrior = list_warriors[1].GetComponent<Warrior>();
        }
        else if (tp == warrior_type_enum.level_3)
        {
            list_warriors[2].SetActive(true);
            active_warrior = list_warriors[2].GetComponent<Warrior>();
        }
        else if (tp == warrior_type_enum.level_4)
        {
            list_warriors[3].SetActive(true);
            active_warrior = list_warriors[3].GetComponent<Warrior>();
        }
        else if (tp == warrior_type_enum.level_5)
        {
            list_warriors[4].SetActive(true);
            active_warrior = list_warriors[4].GetComponent<Warrior>();
        }
        else if (tp == warrior_type_enum.level_6)
        {
            list_warriors[5].SetActive(true);
            active_warrior = list_warriors[5].GetComponent<Warrior>();
        }
        else if (tp == warrior_type_enum.level_7)
        {
            list_warriors[6].SetActive(true);
            active_warrior = list_warriors[6].GetComponent<Warrior>();
        }
        else if (tp == warrior_type_enum.level_8)
        {
            list_warriors[7].SetActive(true);
            active_warrior = list_warriors[7].GetComponent<Warrior>();
        }
        else if (tp == warrior_type_enum.level_9)
        {
            list_warriors[8].SetActive(true);
            active_warrior = list_warriors[8].GetComponent<Warrior>();
        }
    }

    public void upgrate_level_and_active__warrior(warrior_type_enum tp)
    {
        if (tp == warrior_type_enum.level_1)
        {
            hide_all_warriors();
            list_warriors[1].SetActive(true);
            active_warrior = list_warriors[1].GetComponent<Warrior>();
            warrior_type = warrior_type_enum.level_2;

            // show panel new character warrior
            if(GameManager.instance.get_count_active_warrior() == 1)
            {
                GameManager.instance.set_count_active_warrior(GameManager.instance.get_count_active_warrior() + 1);
                UiManager.instance.show_panel_new_character_warrior();

            }
        }
        else if (tp == warrior_type_enum.level_2)
        {
            hide_all_warriors();
            list_warriors[2].SetActive(true);
            active_warrior = list_warriors[2].GetComponent<Warrior>();
            warrior_type = warrior_type_enum.level_3;

            // show panel new character warrior
            if (GameManager.instance.get_count_active_warrior() == 2)
            {
                GameManager.instance.set_count_active_warrior(GameManager.instance.get_count_active_warrior() + 1);
                UiManager.instance.show_panel_new_character_warrior();

            }
        }
        else if (tp == warrior_type_enum.level_3)
        {
            hide_all_warriors();
            list_warriors[3].SetActive(true);
            active_warrior = list_warriors[3].GetComponent<Warrior>();
            warrior_type = warrior_type_enum.level_4;

            // show panel new character warrior
            if (GameManager.instance.get_count_active_warrior() == 3)
            {
                GameManager.instance.set_count_active_warrior(GameManager.instance.get_count_active_warrior() + 1);
                UiManager.instance.show_panel_new_character_warrior();

            }
        }
        else if (tp == warrior_type_enum.level_4)
        {
            hide_all_warriors();
            list_warriors[4].SetActive(true);
            active_warrior = list_warriors[4].GetComponent<Warrior>();
            warrior_type = warrior_type_enum.level_5;

            // show panel new character warrior
            if (GameManager.instance.get_count_active_warrior() == 4)
            {
                GameManager.instance.set_count_active_warrior(GameManager.instance.get_count_active_warrior() + 1);
                UiManager.instance.show_panel_new_character_warrior();

            }
        }
        else if (tp == warrior_type_enum.level_5)
        {
            hide_all_warriors();
            list_warriors[5].SetActive(true);
            active_warrior = list_warriors[5].GetComponent<Warrior>();
            warrior_type = warrior_type_enum.level_6;

            // show panel new character warrior
            if (GameManager.instance.get_count_active_warrior() == 5)
            {
                GameManager.instance.set_count_active_warrior(GameManager.instance.get_count_active_warrior() + 1);
                UiManager.instance.show_panel_new_character_warrior();

            }
        }
        else if (tp == warrior_type_enum.level_6)
        {
            hide_all_warriors();
            list_warriors[6].SetActive(true);
            active_warrior = list_warriors[6].GetComponent<Warrior>();
            warrior_type = warrior_type_enum.level_7;

            // show panel new character warrior
            if (GameManager.instance.get_count_active_warrior() == 6)
            {
                GameManager.instance.set_count_active_warrior(GameManager.instance.get_count_active_warrior() + 1);
                UiManager.instance.show_panel_new_character_warrior();

            }
        }
        else if (tp == warrior_type_enum.level_7)
        {
            hide_all_warriors();
            list_warriors[7].SetActive(true);
            active_warrior = list_warriors[7].GetComponent<Warrior>();
            warrior_type = warrior_type_enum.level_8;

            // show panel new character warrior
            if (GameManager.instance.get_count_active_warrior() == 7)
            {
                GameManager.instance.set_count_active_warrior(GameManager.instance.get_count_active_warrior() + 1);
                UiManager.instance.show_panel_new_character_warrior();

            }
        }
        else if (tp == warrior_type_enum.level_8)
        {
            hide_all_warriors();
            list_warriors[8].SetActive(true);
            active_warrior = list_warriors[8].GetComponent<Warrior>();
            warrior_type = warrior_type_enum.level_9;

            // show panel new character warrior
            if (GameManager.instance.get_count_active_warrior() == 8)
            {
                GameManager.instance.set_count_active_warrior(GameManager.instance.get_count_active_warrior() + 1);
                UiManager.instance.show_panel_new_character_warrior();

            }
        }
        
        else
        {
            // no more upgrate 
            print("end upgrate");
        }
    }
}