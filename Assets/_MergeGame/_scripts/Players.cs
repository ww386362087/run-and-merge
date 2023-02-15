using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public List<Monster> list_active_monsters;
    public List<Warrior> list_active_warriors;
    public Enemies enemies_script;
    GameController game_controller_script;

    // Start is called before the first frame update
    void Start()
    {
        game_controller_script = FindObjectOfType<GameController>();

        //choose_enemy_for_fight_start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void choose_enemy_for_fight_start()
    {
        //monster
        for (int i = 0; i < list_active_monsters.Count; i++)
        {
            //active navemesh of all players
            list_active_monsters[i].agent.enabled = true;
            //set destination
            if(enemies_script.get_active_monster() != null)
            {
                list_active_monsters[i].set_destination(enemies_script.get_active_monster().transform);
            }
            else if (enemies_script.get_active_warrior() != null)
            {
                list_active_monsters[i].set_destination(enemies_script.get_active_warrior().transform);
            }


        }

        // warrior
        for (int i = 0; i < list_active_warriors.Count; i++)
        {
            //set animation
            list_active_warriors[i].animate_warrior("hit");
        }
        //get_active_monster().set_destination(enemies_script.get_active_monster().transform);
    }

    public void choose_another_enemy_for_fight()
    {
        get_active_monster().set_destination(enemies_script.get_active_monster().transform);
    }

    public Monster get_active_monster()
    {
        Monster mons = null;

        if (list_active_monsters.Count > 0)
        {
            mons = list_active_monsters[Random.Range(0, list_active_monsters.Count)];
        }

        return mons;
    }

    public Warrior get_active_warrior()
    {
        Warrior warr = null;

        if (list_active_warriors.Count > 0)
        {
            warr = list_active_warriors[Random.Range(0, list_active_warriors.Count)];
        }

        return warr;
    }


    public void remove_from_active_monsters(Monster mons)
    {
        list_active_monsters.Remove(mons);

        //check lose
        check_if_all_players_died();

        //if (list_active_monsters.Count == 0 && list_active_warriors.Count == 0)
        //{
        //    // enemy win game
        //    //panel lose
        //    print("you lose");
        //}
    }

    public void remove_from_active_warriors(Warrior war)
    {
        list_active_warriors.Remove(war);

        //check lose
        check_if_all_players_died();
    }

    public void check_if_all_players_died()
    {
        if (list_active_warriors.Count == 0 && list_active_monsters.Count == 0 && game_controller_script.game_run)
        {
            game_controller_script.game_run = false;

            //stop players
            stop_all_players();

            enemies_script.stop_all_enemies();

            // enemy win game
            //panel lose
            UiManager.instance.show_lose();
            // animate all enemies win
            enemies_script.win_animation();



            print("you lose");
        }
    }

    public void stop_all_players()
    {
        // monster player
        for (int i = 0; i < list_active_monsters.Count; i++)
        {
            list_active_monsters[i].agent.enabled = false;
        }


        // warrior
        //for (int i = 0; i < list_active_warriors.Count; i++)
        //{
        //    list_active_warriors[i].agent.enabled = false;
        //}
    }

    public void win_animation()
    {
        // monster

        for (int i = 0; i < list_active_monsters.Count; i++)
        {
            list_active_monsters[i].animate_monster("win");
        }
        //warrior
        for (int i = 0; i < list_active_warriors.Count; i++)
        {
            list_active_warriors[i].animate_warrior("win");
        }
    }

    public void change_monster(Monster mns_from , Monster mns_to)
    {
        for (int i = 0; i < list_active_monsters.Count; i++)
        {
            if(list_active_monsters[i] == mns_from)
            {
                list_active_monsters[i] = mns_to;
                break;
            }
        }
    }

    public void change_warrior(Warrior war_from, Warrior war_to)
    {
        for (int i = 0; i < list_active_warriors.Count; i++)
        {
            if (list_active_warriors[i] == war_from)
            {
                list_active_warriors[i] = war_to;
                break;
            }
        }
    }


    public void remove_from_list_monster(Monster mns)
    {
        for (int i = 0; i < list_active_monsters.Count; i++)
        {
            if(list_active_monsters[i] == mns)
            {
                list_active_monsters.Remove(list_active_monsters[i]);
                break;
            }
            else
                print("none");
        }
    }

    public void remove_from_list_warrior(Warrior war)
    {
        for (int i = 0; i < list_active_warriors.Count; i++)
        {
            if (list_active_warriors[i] == war)
            {
                list_active_warriors.Remove(list_active_warriors[i]);
                break;
            }
            else
                print("none");
        }
    }


    public void add_to_list_active_monster(Monster mns)
    {
        list_active_monsters.Add(mns);
    }

    public void add_to_list_active_warrior(Warrior war)
    {
        list_active_warriors.Add(war);
    }
}
