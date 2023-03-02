using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using DG.Tweening;

public class Warrior : MonoBehaviour
{
    public int damage, health , max_health;
    public GameObject canvas_parrent;
    public Image bar_field;
    public Transform target;
    public GameObject arrow_animation;
    public GameObject[] arrows;
    public int current_arrow_index;
    public float time_between_hit;
    //public NavMeshAgent agent;
    public Animator anim;
    public bool active , fight, is_enemy;

    Enemies enemies_script;
    Players Players_script;
    GameController game_controller_script;

    public TMPro.TextMeshPro text_pref;
    public TMPro.TMP_FontAsset font;
    public int hit_coin;
    string current_anim = "_idle";

    //const string run = "_body";
    const string death = "_dead";
    const string hit = "_buff_002";
    const string win = "_buff_001";

    // Start is called before the first frame update
    void Start()
    {
        //hit_coin = 10;

        active = true;

        UpdateAnim();
        max_health = health;

        game_controller_script = FindObjectOfType<GameController>();
        enemies_script = FindObjectOfType<Enemies>();
        Players_script = FindObjectOfType<Players>();

        //agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        if (target != null && game_controller_script.game_run)
        {
            Vector3 tmp_pos = target.position;
            tmp_pos.y = transform.position.y;

            if (is_enemy)
            {
                transform.DOLookAt(tmp_pos, .5f, AxisConstraint.Y);
            }
            else
            {
                transform.parent.DOLookAt(tmp_pos, .5f, AxisConstraint.Y);
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


    [ContextMenu("Update font")]
    private void Updatefont()
    {
        var t = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        t.font = font;
        t.text = $"Gunner {transform.name.Split('_')[1]}";
        t.fontSize = 30;
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
                print("warr remove");
                Players_script.remove_from_active_warriors(this);
            }
            else
            {
                enemies_script.remove_from_active_warriors(this);
            }

            animate_warrior(death);
        }
    }

    public void animate_warrior(string name_anim)
    {
        if (name_anim != current_anim)
        {
            anim.Play(name_anim);
            anim.SetFloat("Velocity", 1);
            current_anim = name_anim;
        }
    }

    public void ChangeSpeed(float speed)
    {
        anim.SetFloat("Velocity", speed);
    }

    public void active_arrow()
    {
        arrow_animation.SetActive(true);
    }

    public void fight_arrow()
    {
        // sound
        SoundManager.instance.Play("arrow");

        // for enemy
        if (is_enemy)
        {
            //if (!game_controller_script.game_run) return;

            // for monster
            if (Players_script.get_active_monster() != null)
            {
                target = Players_script.get_active_monster().transform;


                if (arrow_animation != null)
                {
                    arrow_animation.SetActive(false);
                }
                arrows[current_arrow_index].SetActive(true);

                Vector3 tmp_pos = target.position;
                tmp_pos.y = transform.position.y;
                var weapon = arrows[current_arrow_index].GetComponent<Weapon>();
                //set enemy
                weapon.enemy = Players_script.get_active_monster().gameObject;
                //set damage
                weapon.damage = damage;
                //arrows[current_arrow_index].GetComponent<Weapon>().is_active = true;
                ////set coin
                //arrows[current_arrow_index].GetComponent<Weapon>().coin = hit_coin;
                //set bool isenemy
                weapon.is_enemy = is_enemy;

                if (Vector3.Distance(tmp_pos, transform.position) < 4)
                {
                    weapon.speed_weapon = .05f;
                }
                //else
                //{
                //    arrows[current_arrow_index].GetComponent<Weapon>().speed_weapon = 3f;
                //}
                weapon.start_follow(target);

                if (current_arrow_index < arrows.Length - 1)
                {
                    current_arrow_index++;
                }
                else
                {
                    current_arrow_index = 0;
                }
            }
            // for warrior
            else if (Players_script.get_active_warrior() != null)
            {
                target = Players_script.get_active_warrior().transform;

                //arrow_animation.SetActive(false);
                if (arrow_animation != null)
                {
                    arrow_animation.SetActive(false);
                }
                arrows[current_arrow_index].SetActive(true);
                var weapon = arrows[current_arrow_index].GetComponent<Weapon>();
                Vector3 tmp_pos = target.position;
                tmp_pos.y = transform.position.y;

                //set enemy
                weapon.enemy = Players_script.get_active_warrior().gameObject;
                //set damage
                weapon.damage = damage;

                //arrows[current_arrow_index].GetComponent<Weapon>().is_active = true;

                ////set coin
                //arrows[current_arrow_index].GetComponent<Weapon>().hit_coin = hit_coin;
                //set bool isenemy
                weapon.is_enemy = is_enemy;

                if (Vector3.Distance(tmp_pos, transform.position) < 4)
                {
                    weapon.speed_weapon = .05f;
                }
                weapon.start_follow(target);

                if (current_arrow_index < arrows.Length - 1)
                {
                    current_arrow_index++;
                }
                else
                {
                    current_arrow_index = 0;
                }
            }
            else
            {
                print("finish game, warrior -> player");
            }
        }
        // for player
        else if (!is_enemy)
        {
            // for monster
            if (enemies_script.get_active_monster() != null)
            {
                target = enemies_script.get_active_monster().transform;
                print(target.name);

                if (arrow_animation != null)
                {
                    arrow_animation.SetActive(false);
                }

                arrows[current_arrow_index].SetActive(true);

                Vector3 tmp_pos = target.position;
                tmp_pos.y = transform.position.y;
                var weapon = arrows[current_arrow_index].GetComponent<Weapon>();
                //set enemy
                weapon.enemy = enemies_script.get_active_monster().gameObject;
                //set damage
                weapon.damage = damage;

                //arrows[current_arrow_index].GetComponent<Weapon>().is_active = true;

                //set coin
                weapon.hit_coin = hit_coin;
                //set bool isenemy
                weapon.is_enemy = is_enemy;

                if (Vector3.Distance(tmp_pos, transform.position) < 4)
                {
                    weapon.speed_weapon = .05f;
                }
                //else
                //{
                //    arrows[current_arrow_index].GetComponent<Weapon>().speed_weapon = 3f;
                //}
                weapon.start_follow(target);

                if (current_arrow_index < arrows.Length - 1)
                {
                    current_arrow_index++;
                }
                else
                {
                    current_arrow_index = 0;
                }
            }
            // for warrior
            else if (enemies_script.get_active_warrior() != null)
            {
                target = enemies_script.get_active_warrior().transform;

                //arrow_animation.SetActive(false);
                if (arrow_animation != null)
                {
                    arrow_animation.SetActive(false);
                }

                arrows[current_arrow_index].SetActive(true);

                Vector3 tmp_pos = target.position;
                tmp_pos.y = transform.position.y;
                var weapon = arrows[current_arrow_index].GetComponent<Weapon>();
                //set enemy
                weapon.enemy = enemies_script.get_active_warrior().gameObject;
                //set damage
                weapon.damage = damage;

                //weapon.is_active = true;

                //set coin
                weapon.hit_coin = hit_coin;
                //set bool isenemy
                weapon.is_enemy = is_enemy;

                if (Vector3.Distance(tmp_pos, transform.position) < 4)
                {
                    weapon.speed_weapon = .05f;
                }
                weapon.start_follow(target);

                if (current_arrow_index < arrows.Length - 1)
                {
                    current_arrow_index++;
                }
                else
                {
                    current_arrow_index = 0;
                }
            }
            else
            {
                print("finish game, warrior -> player");

                for (int i = 0; i < Players_script.list_active_warriors.Count; i++)
                {
                    Players_script.list_active_warriors[i].animate_warrior(win);
                }

                for (int i = 0; i < enemies_script.list_active_warriors.Count; i++)
                {
                    enemies_script.list_active_warriors[i].animate_warrior(death);
                }

            }
        }
    }
}
