using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Weapon : MonoBehaviour
{
    public float speed_weapon;
    public Vector3 target;
    public GameObject enemy;
    public int damage , hit_coin;
    public bool is_active , is_enemy;
    public Ease ease;
    Vector3 first_pos;

    GameController game_controller_script;
    //Tweener tween;
    // Start is called before the first frame update
    void Start()
    {
        //is_active = false;

        game_controller_script = FindObjectOfType<GameController>();

        speed_weapon = .2f;
        first_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!is_active) return;

        //tween.ChangeEndValue(target, true).OnComplete(() => on_reached());

        if (is_active)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, 35 * Time.deltaTime);

            if (!game_controller_script.game_run)
            {
                is_active = false;
                gameObject.SetActive(false);
                return;
            }
            if (Vector3.Distance(transform.position, enemy.transform.position) <= .05f)
            {
                on_reached();
            }
        }
    }

    public void start_follow(Transform tr)
    {

        print("start follow");
        // khass ndir look at target

        //target = tr;
        //print(target);
        //target.y = transform.position.y;

        is_active = true;

        //transform.DOMove(target , speed_weapon).OnComplete(() => on_reached());

        //tween = transform.DOMove(target, speed_weapon).SetEase(ease).SetAutoKill(false);

        //is_active = true;
    }

    public void on_reached()
    {
        if (game_controller_script.game_run)
        {
            if (!is_active) return;

            print("reached");
            //monster
            if (enemy.GetComponent<Monster>() != null)
            {
                if (enemy.GetComponent<Monster>().active)
                {
                    enemy.GetComponent<Monster>().decrease_health(damage);
                }

                if (!is_enemy)
                {
                    instantiate_text_coin(enemy.GetComponent<Monster>().text_pref);
                }
            }
            //warrior
            else if (enemy.GetComponent<Warrior>() != null)
            {
                if (enemy.GetComponent<Warrior>().active)
                {
                    enemy.GetComponent<Warrior>().decrease_health(damage);
                }

                if (!is_enemy)
                {
                    instantiate_text_coin(enemy.GetComponent<Warrior>().text_pref);
                }
            }
        }



        transform.position = first_pos;
        gameObject.SetActive(false);
        is_active = false;
    }

    public void instantiate_text_coin(TMPro.TextMeshPro pref)
    {
        print("coins");
        TMPro.TextMeshPro txt = Instantiate(pref, enemy.transform.position, Quaternion.identity, enemy.transform);
        Vector3 tmp = txt.rectTransform.anchoredPosition;
        tmp.x = Random.Range(-1f, 1f);
        txt.rectTransform.anchoredPosition = tmp;

        UiManager.instance.calcul_total_coin_in_level(hit_coin);

        txt.text = hit_coin + "$";

        Destroy(txt, 1f);
        UiManager.instance.increase_money(hit_coin);
    }
}
