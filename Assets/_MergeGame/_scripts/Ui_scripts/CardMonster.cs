using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardMonster : MonoBehaviour
{
    public List<GameObject> list_monster_cards;

    // Start is called before the first frame update
    void Start()
    {
        unlock_active_monsters();
    }
    

    public void unlock_active_monsters()
    {
        int cnt = GameManager.instance.get_count_active_monster();
        cnt =  Mathf.Clamp(cnt, 0, list_monster_cards.Count);

        for (int i = 1; i < cnt; i++)
        {
            //active background
            list_monster_cards[i].GetComponent<Image>().enabled = true;

            //inactive locked image
            list_monster_cards[i].transform.GetChild(3).gameObject.SetActive(false);
        }
    }
}
