using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public List<Monster> list_active_monsters;
    public List<Warrior> list_active_warriors;
    public Players Players_script;
    GameController game_controller_script;

    const string win = "_buff_001";
    const string hit_range = "_buff_002";

    // Start is called before the first frame update
    void Start()
    {
        game_controller_script = FindObjectOfType<GameController>();

        //choose_player_for_fight_start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void choose_player_for_fight_start()
    {

        for (int i = 0; i < list_active_monsters.Count; i++)
        {
            list_active_monsters[i].set_destination(Players_script.get_active_monster().transform);
        }

        // warrior
        for (int i = 0; i < list_active_warriors.Count; i++)
        {
            //set animation
            list_active_warriors[i].animate_warrior(hit_range);
        }

        //get_active_monster().set_destination(Players_script.get_active_monster().transform);
    }

    public void choose_another_player_for_fight()
    {
        get_active_monster().set_destination(Players_script.get_active_monster().transform);
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

        //check win
        check_if_all_enemies_died();

        //if (list_active_monsters.Count == 0 && list_active_warriors.Count == 0)
        //{
        //    // player win game
        //    //panel win
        //    print("you win");
        //}
    }

    public void remove_from_active_warriors(Warrior war)
    {
        list_active_warriors.Remove(war);

        //check win
        check_if_all_enemies_died();

        //if (list_active_warriors.Count == 0 && list_active_monsters.Count == 0)
        //{
        //    // player win game
        //    //panel win
        //    print("you win");
        //}
    }

    public void check_if_all_enemies_died()
    {
        if (list_active_warriors.Count == 0 && list_active_monsters.Count == 0 && game_controller_script.game_run)
        {
            game_controller_script.game_run = false;

            //stop players
            stop_all_enemies();

            Players_script.stop_all_players();

            // player win game
            //panel win
            UiManager.instance.show_win();

            // animate all players win

            Players_script.win_animation();

            print("you win");
        }
    }

    public void stop_all_enemies()
    {
        // monster
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
            list_active_monsters[i].animate_monster(win);
        }
        //warrior
        for (int i = 0; i < list_active_warriors.Count; i++)
        {
            list_active_warriors[i].animate_warrior(win);
        }

        // disable 
    }

    
}
