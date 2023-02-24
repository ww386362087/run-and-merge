using HyperCasual.Core;
using HyperCasual.Gameplay;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameController : Singleton<GameController> , IGameEventListener
{
    public FinishRunEvent evt;

    public GameObject previous_object, current_object , clicked_object;
    public LayerMask cadre_layer , ground_layer;
    public Camera cam;
    public Vector3 current_position_ray;
    public bool can_move , game_run;
    public List<Cadre> list_cadres, list_empty_cadres;

    public string type_move;
    public Transform character;
    public Enemies enemies_script;
    public Players players_scripts;
    public List<GameObject> levels_list;
    public Transform jumpPoint;

    // Start is called before the first frame update
    void Start()
    {
        //load data
        load_data_from_saved_cadres();

        // get level
        get_actual_level();

        // get empty cadres
        //get_list_empty_cadres_start_game();
        
        evt.AddListener(this);
    }

    public void LoadNextLevel()
    {
        //load data
        load_data_from_saved_cadres();

        // get level
        get_actual_level();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameSceneLoad.Instance != null)
        {
            if (!GameSceneLoad.Instance.isFinishRun)
                return;
        }
        if (!game_run) return;

        // test add monster
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    // sound
        //    SoundManager.instance.Play("deploy");

        //    //add_monster_to_scene();
        //    add_warrior_to_scene();
        //}
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    // sound
        //    SoundManager.instance.Play("deploy");

        //    add_monster_to_scene();

        //}
        //fight
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    players_scripts.choose_enemy_for_fight_start();

        //    enemies_script.choose_player_for_fight_start();
        //}
        if (Input.GetMouseButtonDown(0))
        {
            active_move();
        }
        if (Input.GetMouseButton(0))
        {
            select_cadre();
            move_object();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (current_object != null)
            {

                // hide color
                current_object.GetComponent<Cadre>().hide_color_cadre();


                //for monster----------------------------------------------------------
                if (type_move == "monster")
                {
                    // if same cadre --> don't change anything
                    if (current_object == clicked_object)
                    {
                        //print("zzzzzzzzzzzz");
                        // return clicked object to place
                        clicked_object.GetComponent<Cadre>().dinosaur_parent.transform.position = clicked_object.GetComponent<Cadre>().pos.position;
                    }

                    // change details if cadre empty
                    else if (current_object.GetComponent<Cadre>().monster_type == monster_type_enum.none)
                    {
                        // set false to bool has_din
                        clicked_object.GetComponent<Cadre>().set_false_din();

                        //set bool true and set false for previous cadre
                        current_object.GetComponent<Cadre>().set_true_din();
                        //previous_object.GetComponent<Cadre>().set_false_din();

                        // return clicked object to place
                        clicked_object.GetComponent<Cadre>().dinosaur_parent.transform.position = clicked_object.GetComponent<Cadre>().pos.position;

                        //change current object to clicked object
                        current_object.GetComponent<Cadre>().monster_type = clicked_object.GetComponent<Cadre>().monster_type;

                        //hide all monster
                        clicked_object.GetComponent<Cadre>().hide_all_monster();

                        current_object.GetComponent<Cadre>().active_by_type_monster(clicked_object.GetComponent<Cadre>().monster_type);

                        //add cadre to empty cadres
                        get_to_list_empty_cadres(clicked_object.GetComponent<Cadre>());

                        //remove current cadre from empty
                        delete_from_list_cadres(current_object.GetComponent<Cadre>());

                        //change active monster
                        players_scripts.change_monster(clicked_object.GetComponent<Cadre>().active_monster, current_object.GetComponent<Cadre>().active_monster);

                        //reset cadre
                        clicked_object.GetComponent<Cadre>().monster_type = monster_type_enum.none;

                        // save details all cadres
                        save_details_cadres();
                    }
                    //merge if they have same type
                    else if (current_object.GetComponent<Cadre>().monster_type == clicked_object.GetComponent<Cadre>().monster_type)
                    {
                        // play effect
                        current_object.GetComponent<Cadre>().effect_two.Play();

                        // set false to bool has_din
                        clicked_object.GetComponent<Cadre>().has_din = false;

                        // delete current_object and clicked object from list active 
                        players_scripts.remove_from_list_monster(clicked_object.GetComponent<Cadre>().active_monster);
                        players_scripts.remove_from_list_monster(current_object.GetComponent<Cadre>().active_monster);

                        // return clicked object to place
                        clicked_object.GetComponent<Cadre>().dinosaur_parent.transform.position = clicked_object.GetComponent<Cadre>().pos.position;

                        //hide all monster
                        clicked_object.GetComponent<Cadre>().hide_all_monster();

                        // merge 
                        current_object.GetComponent<Cadre>().upgrate_level_and_active__monster(current_object.GetComponent<Cadre>().monster_type);

                        //add cadre to empty cadres
                        get_to_list_empty_cadres(clicked_object.GetComponent<Cadre>());

                        
                        //add merge object to list monster player
                        players_scripts.add_to_list_active_monster(current_object.GetComponent<Cadre>().active_monster);

                        

                        //reset previous cadre
                        clicked_object.GetComponent<Cadre>().monster_type = monster_type_enum.none;

                        // save details all cadres
                        save_details_cadres();
                    }
                    else
                    {
                        // set last pos
                        character.position = current_object.GetComponent<Cadre>().pos.position;
                    }
                }
                //for warrior----------------------------------------------------------
                else if (type_move == "warrior")
                {
                    // if same cadre --> don't change anything
                    if (current_object == clicked_object)
                    {
                        // return clicked object to place
                        clicked_object.GetComponent<Cadre>().warriors_parent.transform.position = clicked_object.GetComponent<Cadre>().pos.position;
                    }

                    // change details if cadre empty
                    else if (current_object.GetComponent<Cadre>().warrior_type == warrior_type_enum.none)
                    {
                        // set false to bool has_warrior
                        clicked_object.GetComponent<Cadre>().set_false_warrior();

                        //set bool true and set false for previous cadre
                        current_object.GetComponent<Cadre>().set_true_warrior();

                        // return clicked object to place
                        clicked_object.GetComponent<Cadre>().warriors_parent.transform.position = clicked_object.GetComponent<Cadre>().pos.position;

                        //change current object to clicked object
                        current_object.GetComponent<Cadre>().warrior_type = clicked_object.GetComponent<Cadre>().warrior_type;

                        //hide all warriors
                        clicked_object.GetComponent<Cadre>().hide_all_warriors();

                        current_object.GetComponent<Cadre>().active_by_type_warrior(clicked_object.GetComponent<Cadre>().warrior_type);

                        //add cadre to empty cadres
                        get_to_list_empty_cadres(clicked_object.GetComponent<Cadre>());

                        //remove current cadre from empty
                        delete_from_list_cadres(current_object.GetComponent<Cadre>());

                        //change active warrior
                        players_scripts.change_warrior(clicked_object.GetComponent<Cadre>().active_warrior, current_object.GetComponent<Cadre>().active_warrior);

                        //reset cadre
                        clicked_object.GetComponent<Cadre>().warrior_type = warrior_type_enum.none;

                        // save details all cadres
                        save_details_cadres();
                    }
                    //merge if they have same type
                    else if (current_object.GetComponent<Cadre>().warrior_type == clicked_object.GetComponent<Cadre>().warrior_type)
                    {
                        // play effect
                        current_object.GetComponent<Cadre>().effect_two.Play();

                        // set false to bool has_warrior
                        clicked_object.GetComponent<Cadre>().has_warrior = false;

                        // delete current_object and clicked object from list active 
                        players_scripts.remove_from_list_warrior(clicked_object.GetComponent<Cadre>().active_warrior);
                        players_scripts.remove_from_list_warrior(current_object.GetComponent<Cadre>().active_warrior);

                        // return clicked object to place
                        clicked_object.GetComponent<Cadre>().warriors_parent.transform.position = clicked_object.GetComponent<Cadre>().pos.position;

                        //hide all monster
                        clicked_object.GetComponent<Cadre>().hide_all_warriors();

                        // merge 
                        current_object.GetComponent<Cadre>().upgrate_level_and_active__warrior(current_object.GetComponent<Cadre>().warrior_type);

                        //add cadre to empty cadres
                        get_to_list_empty_cadres(clicked_object.GetComponent<Cadre>());

                        //add merge object to list monster player
                        players_scripts.add_to_list_active_warrior(current_object.GetComponent<Cadre>().active_warrior);

                        //reset previous cadre
                        clicked_object.GetComponent<Cadre>().warrior_type = warrior_type_enum.none;

                        // save details all cadres
                        save_details_cadres();
                    }
                    else
                    {
                        // set last pos
                        character.position = current_object.GetComponent<Cadre>().pos.position;
                    }
                }




                //reset active for current
                current_object.GetComponent<Cadre>().set_active_false();

                //reset
                clicked_object = current_object = previous_object = null;
                character = null;
            }

            can_move = false;

            
        }
    }

    void select_cadre()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, cadre_layer) && can_move)
        {
            if ((!hit.collider.GetComponent<Cadre>().has_din && !hit.collider.GetComponent<Cadre>().has_warrior) || hit.collider.GetComponent<Cadre>().active 
                || (hit.collider.GetComponent<Cadre>().monster_type == clicked_object.GetComponent<Cadre>().monster_type && type_move == "monster")
                || (hit.collider.GetComponent<Cadre>().warrior_type == clicked_object.GetComponent<Cadre>().warrior_type && type_move == "warrior")
                )
            {
                

                //if(type_move == "monster")
                //{
                //    if (!(hit.collider.GetComponent<Cadre>().monster_type == clicked_object.GetComponent<Cadre>().monster_type)) return;
                //}
                //else if (type_move == "warrior")
                //{
                //    if (!(hit.collider.GetComponent<Cadre>().warrior_type == clicked_object.GetComponent<Cadre>().warrior_type)) return;
                //}

                print(hit.collider.name);

                current_object = hit.collider.gameObject;

                // active color in current cadre
                current_object.GetComponent<Cadre>().color_cadre();

                if (previous_object == null)
                {
                    previous_object = current_object;
                }
                else if (current_object != previous_object)
                {
                    UiManager.instance._vibrate();

                    //if (type_move == "monster")
                    //{
                    //    //set bool true and set false for previous cadre
                    //    current_object.GetComponent<Cadre>().set_true_din();
                    //    if (!previous_object.GetComponent<Cadre>().has_din)
                    //    {
                    //        previous_object.GetComponent<Cadre>().set_false_din();
                    //    }

                    //} 
                    //else if (type_move == "warrior")
                    //{
                    //    //set bool true and set false for previous cadre
                    //    current_object.GetComponent<Cadre>().set_true_warrior();

                    //    if (!previous_object.GetComponent<Cadre>().has_warrior)
                    //    {
                    //        previous_object.GetComponent<Cadre>().set_false_warrior();
                    //    }


                    //}

                    //set true for current && false for previous
                    current_object.GetComponent<Cadre>().set_active_true();
                    previous_object.GetComponent<Cadre>().set_active_false();

                    //hide color from previous
                    previous_object.GetComponent<Cadre>().hide_color_cadre();

                    previous_object = current_object;
                }
            }
            
        }
    }

    void move_object()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground_layer) && can_move)
        {

            //print(hit.point);
            current_position_ray = hit.point;
            current_position_ray.y = .5f;

            character.position = current_position_ray;

        }
    }

    void active_move()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, cadre_layer))
        {
            //for monster
            if (hit.collider.GetComponent<Cadre>().has_din)
            {
                type_move = "monster";
                can_move = true;
                clicked_object = hit.collider.gameObject;
                character = hit.collider.GetComponent<Cadre>().select_object_to_move().transform;

                hit.collider.GetComponent<Cadre>().set_active_true();
            }
            //for warrior
            else if (hit.collider.GetComponent<Cadre>().has_warrior)
            {
                type_move = "warrior";
                can_move = true;
                clicked_object = hit.collider.gameObject;
                character = hit.collider.GetComponent<Cadre>().select_object_to_move_warrior().transform;

                hit.collider.GetComponent<Cadre>().set_active_true();
            }
        }
    }

    public void get_list_empty_cadres_start_game()
    {

        // khass nchof 3lach hadi ma khadamach // 3lach ma kitzadoch fi empty
        for (int i = 0; i < list_cadres.Count; i++)
        {
            if(list_cadres[i].has_din == false && list_cadres[i].has_warrior == false)
            {
                list_empty_cadres.Add(list_cadres[i]);
            }
        }
    }

    public void get_to_list_empty_cadres(Cadre cdr)
    {
        list_empty_cadres.Add(cdr);
    }

    // monster ---------------------------
    public void add_monster_to_scene()
    {
        if (list_empty_cadres.Count > 0)
        {
            //add monster to scene
            list_empty_cadres[0].add_monster();

            //play effect
            //list_empty_cadres[0].effect_one.Play();

            // remove fro list
            delete_from_list_cadres(list_empty_cadres[0]);
        }
        else
        {
            //empty
            print("list is empty");

            AddUnusedFreeMonster();
        }
    }

    void AddUnusedFreeMonster()
    {
        int freeMons = PlayerPrefs.GetInt(GameManager.instance.Num_Free_Mons) + 1;

        Debug.Log("saved free monster: " + freeMons);

        PlayerPrefs.SetInt(GameManager.instance.Num_Free_Mons, freeMons);
    }

    public void add_monster_needed_to_add(int numberOfMonsterToAdd)
    {
        for (int i = 0; i < numberOfMonsterToAdd; i++)
        {
            add_monster_to_scene();
        }

        save_details_cadres();
    }

    public void delete_from_list_cadres(Cadre cdr)
    {
        list_empty_cadres.Remove(cdr);
    }

    // warrior ---------------------------
    public void add_warrior_to_scene()
    {
        if (list_empty_cadres.Count > 0)
        {
            //add warrior to scene
            list_empty_cadres[0].add_warrior();

            //play effect
            //list_empty_cadres[0].effect_one.Play();

            // remove fro list
            delete_from_list_cadres(list_empty_cadres[0]);
        }
        else
        {
            //empty
            print("list is empty");
        }
    }
    
    public void save_details_cadres()
    {
        for (int i = 0; i < list_cadres.Count; i++)
        {

            if (list_cadres[i].has_warrior)
            {
                if (list_cadres[i].warrior_type == warrior_type_enum.level_1)
                {
                    GameManager.instance.set_save_warrior(i, 1);
                    continue;
                }
                else if (list_cadres[i].warrior_type == warrior_type_enum.level_2)
                {
                    GameManager.instance.set_save_warrior(i, 2);
                    continue;
                }
                else if (list_cadres[i].warrior_type == warrior_type_enum.level_3)
                {
                    GameManager.instance.set_save_warrior(i, 3);
                    continue;
                }
                else if (list_cadres[i].warrior_type == warrior_type_enum.level_4)
                {
                    GameManager.instance.set_save_warrior(i, 4);
                    continue;
                }
                else if (list_cadres[i].warrior_type == warrior_type_enum.level_5)
                {
                    GameManager.instance.set_save_warrior(i, 5);
                    continue;
                }
                else if (list_cadres[i].warrior_type == warrior_type_enum.level_6)
                {
                    GameManager.instance.set_save_warrior(i, 6);
                    continue;
                }
                else if (list_cadres[i].warrior_type == warrior_type_enum.level_7)
                {
                    GameManager.instance.set_save_warrior(i, 7);
                    continue;
                }
                else if (list_cadres[i].warrior_type == warrior_type_enum.level_8)
                {
                    GameManager.instance.set_save_warrior(i, 8);
                    continue;
                }
                else if (list_cadres[i].warrior_type == warrior_type_enum.level_9)
                {
                    GameManager.instance.set_save_warrior(i, 9);
                    continue;
                }
            }
            else
            {
                GameManager.instance.set_save_warrior(i, 0);
                //continue;
            }

            if (list_cadres[i].has_din)
            {
                if (list_cadres[i].monster_type == monster_type_enum.level_1)
                {
                    print("add list monster");
                    GameManager.instance.set_save_monster(i, 1);
                    continue;
                }
                else if (list_cadres[i].monster_type == monster_type_enum.level_2)
                {
                    GameManager.instance.set_save_monster(i, 2);
                    continue;
                }
                else if (list_cadres[i].monster_type == monster_type_enum.level_3)
                {
                    GameManager.instance.set_save_monster(i, 3);
                    continue;
                }
                else if (list_cadres[i].monster_type == monster_type_enum.level_4)
                {
                    GameManager.instance.set_save_monster(i, 4);
                    continue;
                }
                else if (list_cadres[i].monster_type == monster_type_enum.level_5)
                {
                    GameManager.instance.set_save_monster(i, 5);
                    continue;
                }
                else if (list_cadres[i].monster_type == monster_type_enum.level_6)
                {
                    GameManager.instance.set_save_monster(i, 6);
                    continue;
                }
                else if (list_cadres[i].monster_type == monster_type_enum.level_7)
                {
                    GameManager.instance.set_save_monster(i, 7);
                    continue;
                }
                else if (list_cadres[i].monster_type == monster_type_enum.level_8)
                {
                    GameManager.instance.set_save_monster(i, 8);
                    continue;
                }
                else if (list_cadres[i].monster_type == monster_type_enum.level_9)
                {
                    GameManager.instance.set_save_monster(i, 9);
                    continue;
                }
                else if (list_cadres[i].monster_type == monster_type_enum.level_10)
                {
                    GameManager.instance.set_save_monster(i, 10);
                    continue;
                }
                
            }
            else
            {
                GameManager.instance.set_save_monster(i, 0);
                //continue;
            }
        }
    }

    public void load_data_from_saved_cadres()
    {

        //for (int i = 0; i < list_cadres.Count; i++)
        //{
        //    print("cadre " + i + " = monster ->" + GameManager.instance.get_save_monster(i));
        //    //print("cadre " + i + " = warrior ->" + GameManager.instance.get_save_warrior(i));
        //}

        for (int i = 0; i < list_cadres.Count; i++)
        {
            // if cadre is empty
            if (GameManager.instance.get_save_warrior(i) == 0 && GameManager.instance.get_save_monster(i) == 0)
            {
                // warrior
                list_cadres[i].warrior_type = warrior_type_enum.none;
                //list_cadres[i].active_warrior = list_cadres[i].list_warriors[0].GetComponent<Warrior>();

                // monster
                list_cadres[i].monster_type = monster_type_enum.none;
                //list_cadres[i].active_monster = list_cadres[i].list_dinosaurs[0].GetComponent<Monster>();

                // add to list empty
                list_empty_cadres.Add(list_cadres[i]);

                continue;
            }

            // ---------------- monster -----------------------

            if (GameManager.instance.get_save_monster(i) == 1)
            {
                list_cadres[i].monster_type = monster_type_enum.level_1;
                list_cadres[i].active_monster = list_cadres[i].list_dinosaurs[0].GetComponent<Monster>();
                list_cadres[i].has_din = true;

                //active monster
                list_cadres[i].active_monster.gameObject.SetActive(true);

                players_scripts.list_active_monsters.Add(list_cadres[i].active_monster);
                continue;
            }
            else if (GameManager.instance.get_save_monster(i) == 2)
            {
                list_cadres[i].monster_type = monster_type_enum.level_2;
                list_cadres[i].active_monster = list_cadres[i].list_dinosaurs[1].GetComponent<Monster>();
                list_cadres[i].has_din = true;

                //active monster
                list_cadres[i].active_monster.gameObject.SetActive(true);

                players_scripts.list_active_monsters.Add(list_cadres[i].active_monster);
                continue;
            }
            else if (GameManager.instance.get_save_monster(i) == 3)
            {
                list_cadres[i].monster_type = monster_type_enum.level_3;
                list_cadres[i].active_monster = list_cadres[i].list_dinosaurs[2].GetComponent<Monster>();
                list_cadres[i].has_din = true;

                //active monster
                list_cadres[i].active_monster.gameObject.SetActive(true);

                players_scripts.list_active_monsters.Add(list_cadres[i].active_monster);
                continue;
            }
            else if (GameManager.instance.get_save_monster(i) == 4)
            {
                list_cadres[i].monster_type = monster_type_enum.level_4;
                list_cadres[i].active_monster = list_cadres[i].list_dinosaurs[3].GetComponent<Monster>();
                list_cadres[i].has_din = true;

                //active monster
                list_cadres[i].active_monster.gameObject.SetActive(true);

                players_scripts.list_active_monsters.Add(list_cadres[i].active_monster);
                continue;
            }
            else if (GameManager.instance.get_save_monster(i) == 5)
            {
                list_cadres[i].monster_type = monster_type_enum.level_5;
                list_cadres[i].active_monster = list_cadres[i].list_dinosaurs[4].GetComponent<Monster>();
                list_cadres[i].has_din = true;

                //active monster
                list_cadres[i].active_monster.gameObject.SetActive(true);

                players_scripts.list_active_monsters.Add(list_cadres[i].active_monster);
                continue;
            }
            else if (GameManager.instance.get_save_monster(i) == 6)
            {
                list_cadres[i].monster_type = monster_type_enum.level_6;
                list_cadres[i].active_monster = list_cadres[i].list_dinosaurs[5].GetComponent<Monster>();
                list_cadres[i].has_din = true;

                //active monster
                list_cadres[i].active_monster.gameObject.SetActive(true);

                players_scripts.list_active_monsters.Add(list_cadres[i].active_monster);
                continue;
            }
            else if (GameManager.instance.get_save_monster(i) == 7)
            {
                list_cadres[i].monster_type = monster_type_enum.level_7;
                list_cadres[i].active_monster = list_cadres[i].list_dinosaurs[6].GetComponent<Monster>();
                list_cadres[i].has_din = true;

                //active monster
                list_cadres[i].active_monster.gameObject.SetActive(true);

                players_scripts.list_active_monsters.Add(list_cadres[i].active_monster);
                continue;
            }
            else if (GameManager.instance.get_save_monster(i) == 8)
            {
                list_cadres[i].monster_type = monster_type_enum.level_8;
                list_cadres[i].active_monster = list_cadres[i].list_dinosaurs[7].GetComponent<Monster>();
                list_cadres[i].has_din = true;

                //active monster
                list_cadres[i].active_monster.gameObject.SetActive(true);

                players_scripts.list_active_monsters.Add(list_cadres[i].active_monster);
                continue;
            }
            else if (GameManager.instance.get_save_monster(i) == 9)
            {
                list_cadres[i].monster_type = monster_type_enum.level_9;
                list_cadres[i].active_monster = list_cadres[i].list_dinosaurs[8].GetComponent<Monster>();
                list_cadres[i].has_din = true;

                //active monster
                list_cadres[i].active_monster.gameObject.SetActive(true);

                players_scripts.list_active_monsters.Add(list_cadres[i].active_monster);
                continue;
            }
            else if (GameManager.instance.get_save_monster(i) == 10)
            {
                list_cadres[i].monster_type = monster_type_enum.level_10;
                list_cadres[i].active_monster = list_cadres[i].list_dinosaurs[9].GetComponent<Monster>();
                list_cadres[i].has_din = true;

                //active monster
                list_cadres[i].active_monster.gameObject.SetActive(true);

                players_scripts.list_active_monsters.Add(list_cadres[i].active_monster);
                continue;
            }

            //------------------ warrior-------------------------------------
            if (GameManager.instance.get_save_warrior(i) == 1)
            {
                list_cadres[i].warrior_type = warrior_type_enum.level_1;
                list_cadres[i].active_warrior = list_cadres[i].list_warriors[0].GetComponent<Warrior>();
                list_cadres[i].has_warrior = true;

                //active warrior
                list_cadres[i].active_warrior.gameObject.SetActive(true);

                players_scripts.list_active_warriors.Add(list_cadres[i].active_warrior);
                continue;
            }
            else if (GameManager.instance.get_save_warrior(i) == 2)
            {
                list_cadres[i].warrior_type = warrior_type_enum.level_2;
                list_cadres[i].active_warrior = list_cadres[i].list_warriors[1].GetComponent<Warrior>();
                list_cadres[i].has_warrior = true;

                //active warrior
                list_cadres[i].active_warrior.gameObject.SetActive(true);

                players_scripts.list_active_warriors.Add(list_cadres[i].active_warrior);
                continue;
            }
            else if (GameManager.instance.get_save_warrior(i) == 3)
            {
                list_cadres[i].warrior_type = warrior_type_enum.level_3;
                list_cadres[i].active_warrior = list_cadres[i].list_warriors[2].GetComponent<Warrior>();
                list_cadres[i].has_warrior = true;

                //active warrior
                list_cadres[i].active_warrior.gameObject.SetActive(true);

                players_scripts.list_active_warriors.Add(list_cadres[i].active_warrior);
                continue;
            }
            else if (GameManager.instance.get_save_warrior(i) == 4)
            {
                list_cadres[i].warrior_type = warrior_type_enum.level_4;
                list_cadres[i].active_warrior = list_cadres[i].list_warriors[3].GetComponent<Warrior>();
                list_cadres[i].has_warrior = true;

                //active warrior
                list_cadres[i].active_warrior.gameObject.SetActive(true);

                players_scripts.list_active_warriors.Add(list_cadres[i].active_warrior);
                continue;
            }
            else if (GameManager.instance.get_save_warrior(i) == 5)
            {
                list_cadres[i].warrior_type = warrior_type_enum.level_5;
                list_cadres[i].active_warrior = list_cadres[i].list_warriors[4].GetComponent<Warrior>();
                list_cadres[i].has_warrior = true;

                //active warrior
                list_cadres[i].active_warrior.gameObject.SetActive(true);

                players_scripts.list_active_warriors.Add(list_cadres[i].active_warrior);
                continue;
            }
            else if (GameManager.instance.get_save_warrior(i) == 6)
            {
                list_cadres[i].warrior_type = warrior_type_enum.level_6;
                list_cadres[i].active_warrior = list_cadres[i].list_warriors[5].GetComponent<Warrior>();
                list_cadres[i].has_warrior = true;

                //active warrior
                list_cadres[i].active_warrior.gameObject.SetActive(true);

                players_scripts.list_active_warriors.Add(list_cadres[i].active_warrior);
                continue;
            }
            else if (GameManager.instance.get_save_warrior(i) == 7)
            {
                list_cadres[i].warrior_type = warrior_type_enum.level_7;
                list_cadres[i].active_warrior = list_cadres[i].list_warriors[6].GetComponent<Warrior>();
                list_cadres[i].has_warrior = true;

                //active warrior
                list_cadres[i].active_warrior.gameObject.SetActive(true);

                players_scripts.list_active_warriors.Add(list_cadres[i].active_warrior);
                continue;
            }
            else if (GameManager.instance.get_save_warrior(i) == 8)
            {
                list_cadres[i].warrior_type = warrior_type_enum.level_8;
                list_cadres[i].active_warrior = list_cadres[i].list_warriors[7].GetComponent<Warrior>();
                list_cadres[i].has_warrior = true;

                //active warrior
                list_cadres[i].active_warrior.gameObject.SetActive(true);

                players_scripts.list_active_warriors.Add(list_cadres[i].active_warrior);
                continue;
            }
            else if (GameManager.instance.get_save_warrior(i) == 9)
            {
                list_cadres[i].warrior_type = warrior_type_enum.level_9;
                list_cadres[i].active_warrior = list_cadres[i].list_warriors[8].GetComponent<Warrior>();
                list_cadres[i].has_warrior = true;

                //active warrior
                list_cadres[i].active_warrior.gameObject.SetActive(true);

                players_scripts.list_active_warriors.Add(list_cadres[i].active_warrior);
                continue;
            }
            
        }
    }

    public void get_actual_level()
    {
        int nbr_lvl = GameManager.instance.getlevel();

        GameObject lvl = Instantiate(levels_list[nbr_lvl],transform);

        //lvl.GetComponent<ManageLevel>().add_to_lists_enemies();
    }

    public void OnEventRaised()
    {
        Debug.Log($"Add {evt.NumberCharacterAdd } character");

        add_monster_needed_to_add(evt.NumberCharacterAdd);
    }
}
