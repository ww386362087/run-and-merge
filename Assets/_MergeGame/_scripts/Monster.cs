using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public int damage , health , max_health;
    public GameObject canvas_parrent;
    public Image bar_field;
    public Transform target;
    public float time_between_hit;
    public Animator anim;
    public NavMeshAgent agent;
    public bool active , fight , is_enemy;

    Enemies enemies_script;
    Players Players_script;
    GameController game_controller_script;

    public TMPro.TextMeshPro text_pref;

    public int hit_coin;
    public string current_anim = "_idle";

    const string run = "Locomotion";
    const string death = "_dead";
    const string hit = "_short_003_new";
    const string win = "_buff_001";

    // Start is called before the first frame update
    void Start()
    {
        //hit_coin = 4;

        max_health = health;
        game_controller_script = FindObjectOfType<GameController>();
        enemies_script = FindObjectOfType<Enemies>();
        Players_script = FindObjectOfType<Players>();

        agent.enabled = false;

        //agent.SetDestination(target.position);
    }

    private void Update()
    {
        if (!game_controller_script.game_run) return;

        if (agent.hasPath)
        {
            // acceleration

            //Vector3 toTarget = agent.steeringTarget - transform.position;
            //float angle = Vector3.Angle(transform.forward, toTarget);
            //float acceleration = angle * agent.speed;
            //acceleration = Mathf.Clamp(acceleration, 0, 100);
            //agent.acceleration = acceleration;
            if (is_enemy)
            {
                Players_script.check_if_all_players_died();

                //check if target still alive
                //check if is a monster
                if (target.GetComponent<Monster>() != null)
                {
                    if (!target.GetComponent<Monster>().active)
                    {
                        if (Players_script.get_active_monster() != null)
                        {
                            set_destination(Players_script.get_active_monster().transform);
                        }
                        //else if (Players_script.get_active_warrior() != null)
                        //{
                        //    //set destination
                        //}
                        else
                        {
                            // you win
                            print("finish");
                        }
                    }
                }
            }
            else
            {
                enemies_script.check_if_all_enemies_died();

                //check if target still alive
                //check if is a warrior

                if (target.GetComponent<Warrior>() != null)
                {
                    if (enemies_script.get_active_monster() != null)
                    {
                        set_destination(enemies_script.get_active_monster().transform);
                    }
                    //else if (enemies_script.get_active_warrior() != null)
                    //{
                    //    //set destination
                    //}
                    else
                    {
                        // you win
                        print("finish");
                    }
                }
                    
            }

            

            if (agent.remainingDistance < 2f && !fight)
            {
                fight = true;
                print("reached");
                agent.enabled = false;
                StartCoroutine(fighting(target.gameObject));
                //anim.SetFloat("Velocity", 0);
            }
            else if(!fight)
            {
                agent.SetDestination(target.position);
            }
            
        }
    }

    [ContextMenu("Update anim")]
    private void UpdateAnim()
    {
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }

    public void decrease_health(int nbr)
    {
        health -= nbr;
        bar_field.fillAmount = (float)health / (float)max_health;

        if (health <= 0)
        {
            active = false;
            // bar tkhwa kolha
            bar_field.fillAmount = 0f;
            bar_field.gameObject.SetActive(false);
            canvas_parrent.SetActive(false);

            // 7aydo mn list active
            if (!is_enemy)
            {
                Players_script.remove_from_active_monsters(this);
            }
            else
            {
                enemies_script.remove_from_active_monsters(this);
            }
            agent.enabled = false;
            animate_monster(death);
        }
    }

    public void set_destination(Transform tar)
    {
        agent.enabled = true;
        // make animation
        animate_monster(run);

        target = tar;
        agent.SetDestination(target.position);
    }

    IEnumerator fighting(GameObject enemy)
    {
        WaitForSeconds wt= new WaitForSeconds(time_between_hit);

        // make animation
        animate_monster(hit);
        print("hit");
        
        
        do
        {
            yield return wt;
            // for monster
            if (enemy.GetComponent<Monster>() != null && active)
            {
                print("in");
                
                if (enemy.GetComponent<Monster>().active)
                {
                    enemy.GetComponent<Monster>().decrease_health(damage);

                    if (!is_enemy)
                    {
                        //instantiate coins
                        instantiate_text_coin(enemy.transform , enemy.GetComponent<Monster>().text_pref);
                    }
                    
                }

                if (!enemy.GetComponent<Monster>().active)
                {
                    // make animation for my player
                    //animate_monster("win");

                    //fight with other 
                    if (is_enemy)
                    {
                        if(Players_script.get_active_monster() != null)
                        {
                            set_destination(Players_script.get_active_monster().transform);
                        }
                        else if (Players_script.get_active_warrior() != null)
                        {
                            //set destination
                            set_destination(Players_script.get_active_warrior().transform);
                        }
                        else
                        {
                            // you win
                            print("finish");
                        }
                    }
                    else
                    {
                        if (enemies_script.get_active_monster() != null)
                        {
                            set_destination(enemies_script.get_active_monster().transform);
                        }
                        else if (enemies_script.get_active_warrior() != null)
                        {
                            //set destination
                            set_destination(enemies_script.get_active_warrior().transform);
                        }
                        else
                        {
                            // you win
                            print("finish");
                        }
                    }

                    // make animation for enemy
                    enemy.GetComponent<Monster>().animate_monster(death);

                    fight = false;
                }
            }
            //for warrior
            else if (enemy.GetComponent<Warrior>() != null && active)
            {
                print("in");

                if (enemy.GetComponent<Warrior>().active)
                {
                    enemy.GetComponent<Warrior>().decrease_health(damage);
                }

                if (!enemy.GetComponent<Warrior>().active)
                {
                    // make animation for my player
                    //animate_monster("win");

                    //fight with other 
                    if (is_enemy)
                    {
                        if (Players_script.get_active_monster() != null)
                        {
                            set_destination(Players_script.get_active_monster().transform);
                        }
                        else if (Players_script.get_active_warrior() != null)
                        {
                            //set destination
                            set_destination(Players_script.get_active_warrior().transform);
                        }
                        else
                        {
                            // you win
                            print("finish");
                        }
                    }
                    else
                    {
                        if (enemies_script.get_active_monster() != null)
                        {
                            set_destination(enemies_script.get_active_monster().transform);
                        }
                        else if (enemies_script.get_active_warrior() != null)
                        {
                            //set destination
                            set_destination(enemies_script.get_active_warrior().transform);
                        }
                        else
                        {
                            // you win
                            print("finish");
                        }
                    }

                    // make animation for enemy
                    enemy.GetComponent<Warrior>().animate_warrior(death);

                    fight = false;
                }
            }

            
        }
        while (fight && active);
        
    }


    public void animate_monster(string name_anim)
    {
        if(current_anim == run)
        {
            Debug.Log($"Override run by {name_anim}");
        }

        if(name_anim != current_anim)
        {
            anim.Play(name_anim);
            anim.SetFloat("Velocity", 1);
            current_anim = name_anim;
        }
    }

    public void instantiate_text_coin(Transform enemy , TMPro.TextMeshPro pref)
    {
        print("coins");
        TMPro.TextMeshPro txt = Instantiate(pref, enemy.position, Quaternion.identity, enemy);
        Vector3 tmp = txt.rectTransform.anchoredPosition;
        tmp.x = Random.Range(-1f,1f);
        txt.rectTransform.anchoredPosition = tmp;

        UiManager.instance.calcul_total_coin_in_level(hit_coin);

        txt.text = hit_coin + "$";

        Destroy(txt, 1f);
        UiManager.instance.increase_money(hit_coin);
    }
}
