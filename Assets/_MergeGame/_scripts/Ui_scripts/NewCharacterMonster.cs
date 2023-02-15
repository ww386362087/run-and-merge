using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCharacterMonster : MonoBehaviour
{
    public List<GameObject> list_monsters;

    // Start is called before the first frame update
    void Start()
    {
        //show_unlock_monster();
    }


    public void show_unlock_monster()
    {
        int cnt = GameManager.instance.get_count_active_monster();

        //active background
        list_monsters[cnt - 2].SetActive(true);

    }
}
