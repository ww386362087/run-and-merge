using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardWarrior : MonoBehaviour
{
    public List<GameObject> list_warrior_cards;

    // Start is called before the first frame update
    void Start()
    {
        unlock_active_warrior();
    }


    public void unlock_active_warrior()
    {
        int cnt = GameManager.instance.get_count_active_warrior();
        cnt =  Mathf.Clamp(cnt, 0, list_warrior_cards.Count);

        for (int i = 1; i < cnt; i++)
        {
            //active background
            list_warrior_cards[i].GetComponent<Image>().enabled = true;

            //inactive locked image
            list_warrior_cards[i].transform.GetChild(3).gameObject.SetActive(false);
        }
    }
}
